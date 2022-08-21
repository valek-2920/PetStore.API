using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pet_Store.Application.Handlers
{
    public enum PermissionTypes
    {
        [Description(PermissionTypeNames.VIEWROLES)]
        VIEWROLES,

        [Description(PermissionTypeNames.WRITEROLES)]
        WRITEROLES,

        [Description(PermissionTypeNames.MANAGEROLES)]
        MANAGEROLES

    }
}
