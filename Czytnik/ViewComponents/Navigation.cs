using Microsoft.AspNetCore.Mvc;
public class Navigation : ViewComponent
{
  public IViewComponentResult Invoke()
  {
    return View("Default");
  }
}
