﻿using QboxNext.Qserver.Core.Interfaces;

namespace Qboxes.Model.Qboxes
{
    public class Qbox
    {
        public string SerialNumber { get; set; }
        public StorageProvider Storageprovider { get; set; }
        public Precision Precision { get; set; }
        public DataStore DataStore { get; set; }
    }
}