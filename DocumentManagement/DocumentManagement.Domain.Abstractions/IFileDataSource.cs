using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DocumentManagement.Domain.Abstractions
{
    interface IFileDataSource
    {
        FileStream Open(string path,
                   FileMode mode,
                   FileAccess access,
                   FileShare share);
    }
}
