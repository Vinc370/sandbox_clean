using dotnet.Data;
using dotnet.Interface;
using Microsoft.AspNetCore.Mvc;

namespace dotnet.Models;

public class UserDataController : Controller
{
    private readonly UserDataInterface userDataInterface;
    public UserDataController(UserDataInterface userDataInterface)
    {
        this.userDataInterface = userDataInterface;
    }
    public async Task<IActionResult> Index()
    {
        IEnumerable<UserData> userData = await userDataInterface.GetAll();
        return View(userData);
    }
    public async Task<IActionResult> View(int id)
    {
        UserData userData = await userDataInterface.GetById(id);
        return View(userData);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        UserData database = await userDataInterface.GetById(id);
        userDataInterface.Delete(database);
        return RedirectToAction("Index");
    }
}
