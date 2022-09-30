using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace EM.InsurePlus.Data
{
    public interface ISQLite
    {
        SQLiteAsyncConnection GetConnection();
    }
}
