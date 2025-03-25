using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BaseProject.Areas.Admin.Controllers.Base;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class AdminBaseController : Controller;