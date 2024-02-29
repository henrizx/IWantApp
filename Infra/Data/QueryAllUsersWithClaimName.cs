using Dapper;
using IWantApp.Endpoints.Employees;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata;

namespace IWantApp.Infra.Data;

public class QueryAllUsersWithClaimName
{
    private readonly IConfiguration configuration;
    
    public QueryAllUsersWithClaimName(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    public IEnumerable<EmployeeResponse> Execute(int page, int rows)
    {
        var db = new SqlConnection(configuration["ConnectionStrings:IWantDb"]);
        var query = @"SELECT A.EMAIL, B.CLAIMVALUE AS Name
                FROM ASPNETUSERS A
                INNER JOIN ASPNETUSERCLAIMS B
                ON A.ID = B.USERID
                AND CLAIMTYPE = 'NAME'
                ORDER BY NAME
                OFFSET (@page -1) * @rows ROWS FETCH NEXT @rows ROWS ONLY";
        return db.Query<EmployeeResponse>(
            query,
            new { page, rows }
            );
    }
}
