using BSEB_CoreAPI.Data;
using BSEB_CoreAPI.Model;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Data.SqlClient;


namespace BSEB_CoreAPI.Services
{
    public class LoginService : ILoginService
    {
        private readonly AppDbContext context;
        //private readonly ILogger<LoginService> _logger;
        private readonly IConfiguration _configuration;
        private static Dictionary<string, string> _resetTokens = new();

        public LoginService(AppDbContext context,IConfiguration configuration)
        {
            this.context = context;
            //_logger = logger;
            _configuration = configuration;
        }
        public string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                foreach (var b in bytes)
                    builder.Append(b.ToString("x2"));
                return builder.ToString();
            }
        }

        public async Task<LoginResult> LoginUserAsync(string username, string password)
        {
            string hashedPassword = ComputeSha256Hash(password);

            var loginResult = new LoginResult();

            try
            {
                using var conn = context.Database.GetDbConnection();
                using var cmd = conn.CreateCommand();

                cmd.CommandText = "sp_LoginUser";
                cmd.CommandType = CommandType.StoredProcedure;

                // INPUT parameters
                cmd.Parameters.Add(new SqlParameter("@Username", username));
                cmd.Parameters.Add(new SqlParameter("@Password", hashedPassword));

                // OUTPUT parameters
                var isSuccessParam = new SqlParameter("@IsSuccess", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(isSuccessParam);

                var messageParam = new SqlParameter("@Message", SqlDbType.NVarChar, 100)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(messageParam);

                if (conn.State != ConnectionState.Open)
                    await conn.OpenAsync();

                LoginUserInfo userInfo = null;

                // READ RESULT SET
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        userInfo = new LoginUserInfo
                        {
                            CollegeId = reader.GetInt32(reader.GetOrdinal("Id")),
                            UserName = reader["UserName"].ToString(),
                            CollegeName = reader["CollegeName"].ToString(),
                            CollegeCode = reader["CollegeCode"].ToString(),
                            DistrictName = reader["DistrictName"].ToString(),
                            DistrictCode = reader["DistrictCode"].ToString(),
                            PrincipalMobileNo = reader["PrincipalMobileNo"].ToString(),
                            EmailId = reader["EmailId"].ToString()
                        };
                    }
                }

                // READ OUTPUT PARAMS
                bool isSuccess = (bool)(isSuccessParam.Value ?? false);
                string message = messageParam.Value?.ToString();

                loginResult.IsSuccess = isSuccess;
                loginResult.Message = message ?? "Unknown error";

                if (isSuccess && userInfo != null)
                {
                    loginResult.CollegeId = userInfo.CollegeId;
                    loginResult.CollegeName = userInfo.CollegeName;
                    loginResult.CollegeCode = userInfo.CollegeCode;
                    loginResult.DistrictName = userInfo.DistrictName;
                    loginResult.DistrictCode = userInfo.DistrictCode;
                    loginResult.PrincipalMobileNo = userInfo.PrincipalMobileNo;
                    loginResult.EmailId = userInfo.EmailId;
                }
            }
            catch (Exception ex)
            {
                loginResult.IsSuccess = false;
                loginResult.Message = "Server error: " + ex.Message;
            }

            return loginResult;
        }


    }
}
