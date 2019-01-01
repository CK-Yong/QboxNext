﻿using Microsoft.AspNetCore.Mvc;

namespace QboxNext.Qserver.Controllers
{
    [ApiController]
    public class FirmwareController : ControllerBase
    {
        // GET firmware/qbox
        // Example: /firmware/qbox/6618-1200-2305/12-45-001-687/R37
        //          /firmware/qbox/6618-1400-0200/15-46-001-414/R48
        [HttpGet("/firmware/qbox/{pn}/{sn}/{rn}")]
        public ActionResult Firmware(string pn, string sn, string rn)
        {
            byte[] bytes = System.IO.File.ReadAllBytes("firmware\\A48_ENCRYPT_ON_svn_ver_681");
            return File(bytes, "application/octet-stream", "A48_ENCRYPT_ON_svn_ver_681");
        }
    }
}
