using System;

namespace ServiceLayer
{
    [Serializable]
    public class RequestBaseMessage
    {
        public string BusinessAction { get; set; }
    }
}
