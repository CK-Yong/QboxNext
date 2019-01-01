using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Qboxes.Classes;
using Qboxes.Interfaces;
using QboxNext.Qserver.Classes;

namespace QboxNext.Qserver.Controllers
{
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IQboxDataDumpContextFactory _qboxDataDumpContextFactory;
        private readonly IQboxMessagesLogger _qboxMessagesLogger;

        public DeviceController(IQboxDataDumpContextFactory qboxDataDataDumpContextFactory)
        {
            _qboxDataDumpContextFactory = qboxDataDataDumpContextFactory;
            _qboxMessagesLogger = new QboxMessagesNullLogger();
        }

        // POST device/qbox
        // Example: /device/qbox/6618-1400-0200/15-46-002-442
        [HttpPost("/device/qbox/{pn}/{sn}")]
        public ActionResult Qbox(string pn, string sn)
        {
            var qboxDataDumpContext = _qboxDataDumpContextFactory.CreateContext(ControllerContext, pn, sn);
            string result = new MiniDataHandler(qboxDataDumpContext, _qboxMessagesLogger).Handle();

            return Ok();
        }
    }
}
