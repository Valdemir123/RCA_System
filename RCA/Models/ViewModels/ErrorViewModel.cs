using System;

namespace RCA.Models.ViewModels
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }
        public string Message { get; set; }
        public GroupType GroupId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}