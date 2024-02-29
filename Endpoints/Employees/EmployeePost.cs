using Microsoft.AspNetCore.Identity;
using System.Security.Claims;


namespace IWantApp.Endpoints.Employees;

public class EmployeePost
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action(EmployeeRequest employeRequest, UserManager<IdentityUser> userManager)
    {
        var user = new IdentityUser {UserName = employeRequest.Email, Email = employeRequest.Email};
        var result = userManager.CreateAsync(user, employeRequest.Password).Result;

        if (!result.Succeeded)
            return Results.ValidationProblem(result.Errors.ConvertToProblemDetails());


        var userClaims = new List<Claim>
        {
            new Claim("EmployeeCode", employeRequest.EmployeeCode),
            new Claim("Name", employeRequest.Name)
        };
        var claimResult =
            userManager.AddClaimsAsync(user,userClaims).Result;
      

        if (!claimResult.Succeeded)
            return Results.BadRequest(claimResult.Errors.First());

     


        return Results.Created($"/employees/{user.Id}", user.Id);

    }
}
