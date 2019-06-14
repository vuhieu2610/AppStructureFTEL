using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AppOutSideAPI.Common
{
    public class StoreProcedureConfigs<TEntity> where TEntity : new()
    {
        private static string _insert_single_store_procedure = "_insert_single_store_procedure";
        private static string _insert_list_store_procedure = "_insert_list_store_procedure";
        private static string _update_single_store_procedure = "_update_single_store_procedure";
        private static string _update_list_store_procedure = "_update_list_store_procedure";
        private static string _delete_single_store_procedure = "_delete_single_store_procedure";
        private static string _delete_list_store_procedure = "_delete_list_store_procedure";
        private static string _get_single_store_procedure = "_get_single_store_procedure";
        private static string _get_paging_store_procedure = "_get_paging_store_procedure";
        private static string _get_all_store_procedure = "_get_all_store_procedure";

        public string INSERT_SINGLE_STORE_PROCEDURE
        {
            get
            {
                var propertyName = string.Format("{0}_{1}", _insert_single_store_procedure, typeof(TEntity).Name);
                return (string)this.GetType().GetProperty(propertyName, BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this, null);
            }
        }

        public string INSERT_LIST_STORE_PROCEDURE
        {
            get
            {
                var propertyName = string.Format("{0}_{1}", _insert_list_store_procedure, typeof(TEntity).Name);
                return (string)this.GetType().GetProperty(propertyName, BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this, null);
            }
        }

        public string UPDATE_SINGLE_STORE_PROCEDURE
        {
            get
            {
                var propertyName = string.Format("{0}_{1}", _update_single_store_procedure, typeof(TEntity).Name);
                return (string)this.GetType().GetProperty(propertyName, BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this, null);
            }
        }

        public string UPDATE_LIST_STORE_PROCEDURE
        {
            get
            {
                var propertyName = string.Format("{0}_{1}", _update_list_store_procedure, typeof(TEntity).Name);
                return (string)this.GetType().GetProperty(propertyName, BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this, null);
            }
        }

        public string DELETE_SINGLE_STORE_PROCEDURE
        {
            get
            {
                var propertyName = string.Format("{0}_{1}", _delete_single_store_procedure, typeof(TEntity).Name);
                return (string)this.GetType().GetProperty(propertyName, BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this, null);
            }
        }

        public string DELETE_LIST_STORE_PROCEDURE
        {
            get
            {
                var propertyName = string.Format("{0}_{1}", _delete_list_store_procedure, typeof(TEntity).Name);
                return (string)this.GetType().GetProperty(propertyName, BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this, null);
            }
        }

        public string GET_ALL_STORE_PROCEDURE
        {
            get
            {
                var propertyName = string.Format("{0}_{1}", _get_all_store_procedure, typeof(TEntity).Name);
                return (string)this.GetType().GetProperty(propertyName, BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this, null);
            }
        }

        public string GET_PAGING_STORE_PROCEDURE
        {
            get
            {
                var propertyName = string.Format("{0}_{1}", _get_paging_store_procedure, typeof(TEntity).Name);
                return (string)this.GetType().GetProperty(propertyName, BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this, null);
            }
        }

        public string GET_SINGLE_STORE_PROCEDURE
        {
            get
            {
                var propertyName = string.Format("{0}_{1}", _get_single_store_procedure, typeof(TEntity).Name);
                return (string)this.GetType().GetProperty(propertyName, BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this, null);
            }
        }

        #region GetPaging

        private string _get_paging_store_procedure_Users
        {
            get
            {
                return "[dbo].[USER_GET_PAGING]";
            }
        }

        #endregion

        #region GetSingle

        private string _get_single_store_procedure_Users
        {
            get
            {
                return "[dbo].[USER_GET_SINGLE]";
            }
        }

        #endregion

        #region InsertList
        private string _insert_list_store_procedure_Users
        {
            get
            {
                return "[dbo].[USER_INSERT]";
            }
        }
        #endregion

        #region InsertSingle
        private string _insert_single_store_procedure_Users
        {
            get
            {
                return "[dbo].[USER_INSERT]";
            }
        }
        #endregion

        #region UpdateList
        private string _update_list_store_procedure_Users
        {
            get
            {
                return "[dbo].[USER_UPDATE]";
            }
        }
        #endregion

        #region UpdateSingle
        private string _update_single_store_procedure_Users
        {
            get
            {
                return "[dbo].[USER_UPDATE]";
            }
        }
        #endregion

        #region DeleteSingle
        private string _delete_single_store_procedure_Users
        {
            get
            {
                return "[dbo].[USER_DELETE_SINGLE]";
            }
        }
        #endregion

    }
}
