﻿using Microsoft.VisualStudio.Data.Framework.AdoDotNet;
using Microsoft.VisualStudio.Data.Services;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmAdo.DdexProvider
{
    /// <summary>
    /// Represents a custom data source information class that is able to
    /// provide data source information values that require some form of
    /// computation, perhaps based on an active connection.
    /// </summary>
    internal class CrmSourceInformation : AdoDotNetSourceInformation
    {
        #region Constructors

        public CrmSourceInformation()
        {
            AddProperty(DefaultSchema);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// RetrieveValue is called once per property that was identified
        /// as existing but without a value (specified in the constructor).
        /// For the purposes of this sample, only one property needs to be
        /// computed - DefaultSchema.  To retrieve this value a SQL statement
        /// is executed.
        /// </summary>
        protected override object RetrieveValue(string propertyName)
        {          

            if (propertyName.Equals("DefaultSchema", StringComparison.OrdinalIgnoreCase))
            {
                if (Site.State != DataConnectionState.Open)
                {
                    Site.Open();
                }
                CrmDbConnection conn = (CrmDbConnection)Connection;
                Debug.Assert(conn != null, "Invalid provider object.");
                if (conn != null)
                {
                    try
                    {
                        return "";
                    }
                    catch (DbException)
                    {
                        // We let the base class apply default behavior
                    }
                }
            }

            var val = base.RetrieveValue(propertyName);
            return val;
          
        }

        #endregion
    }
}
