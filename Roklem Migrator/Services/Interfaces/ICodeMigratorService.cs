﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roklem_Migrator.Services.Interfaces
{
    internal interface ICodeMigratorService
    {
        public void Migrate(IEnumerable<string> codeLines);
    }
}
