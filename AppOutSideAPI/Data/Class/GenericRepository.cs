using AppOutSideAPI.Common;
using AppOutSideAPI.Data.Interfaces;
using EntityData.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;

namespace AppOutSideAPI.Data.Class
{
    public class GenericRepository<T> : IGenericRepository<T> where T : new()
    {
        protected DbProvider _db = null;
        protected StoreProcedureConfigs<T> _storeProcedureConfigs = null;

        public GenericRepository(DbProvider db)
        {
            _db = db;
            _storeProcedureConfigs = new StoreProcedureConfigs<T>();
        }

        public ReturnResult<T> Delete(T item)
        {
            string outCode = string.Empty;
            string outMsg = string.Empty;
            int result = 0;

            _db.SetQuery(_storeProcedureConfigs.DELETE_SINGLE_STORE_PROCEDURE, CommandType.StoredProcedure)
                .SetParameter("IN_JSON", SqlDbType.NVarChar, JsonConvert.SerializeObject(item), 4000, ParameterDirection.Input)
                .SetParameter("OUT_CODE", SqlDbType.NVarChar, DBNull.Value, 40000, ParameterDirection.Output)
                .SetParameter("OUT_MSG", SqlDbType.NVarChar, DBNull.Value, 40000, ParameterDirection.Output)
                .ExecuteNoneQuery(out result);

            _db.GetOutValue("OUT_CODE", out outCode)
               .GetOutValue("OUT_MSG", out outMsg);

            return new ReturnResult<T>()
            {
                Code = outCode,
                Message = outMsg
            };
        }
        

        public ReturnResult<T> GetSingle(T item)
        {
            string outCode = string.Empty;
            string outMsg = string.Empty;

            T Data = new T();

            _db.SetQuery(_storeProcedureConfigs.GET_SINGLE_STORE_PROCEDURE, CommandType.StoredProcedure)
                .SetParameter("IN_JSON", SqlDbType.NVarChar, JsonConvert.SerializeObject(item), 4000, ParameterDirection.Input)
                .SetParameter("OUT_CODE", SqlDbType.NVarChar, DBNull.Value, 0, ParameterDirection.Output)
                .SetParameter("OUT_MSG", SqlDbType.NVarChar, DBNull.Value, 0, ParameterDirection.Output)
                .GetSingle(out Data)
                .Complete();

            _db.GetOutValue("OUT_CODE", out outCode)
               .GetOutValue("OUT_MSG", out outMsg);

            return new ReturnResult<T>()
            {
                Item = Data,
                Code = outCode,
                Message = outMsg
            };
        }

        public ReturnResult<T> GetPaging(BaseCondition condition)
        {
            string outCode = string.Empty;
            string outMsg = string.Empty;
            int outTotalCount = 0;

            List<T> ListData = new List<T>();
            _db.SetQuery(_storeProcedureConfigs.GET_PAGING_STORE_PROCEDURE, CommandType.StoredProcedure)
               .SetParameter("IN_JSON", SqlDbType.NVarChar, JsonConvert.SerializeObject(condition), 4000, ParameterDirection.Input)
               .SetParameter("OUT_TOTAL_COUNT", SqlDbType.Int, DBNull.Value, 4000, ParameterDirection.Output)
               .SetParameter("OUT_CODE", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
               .SetParameter("OUT_MSG", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
               .GetList(out ListData)
               .Complete();

            _db.GetOutValue("OUT_TOTAL_COUNT", out outTotalCount)
               .GetOutValue("OUT_CODE", out outCode)
               .GetOutValue("OUT_MSG", out outMsg);

            return new ReturnResult<T>()
            {
                ListItem = ListData,
                Code = outCode,
                Message = outMsg,
                TotalCount = outTotalCount
            };

        }

        public ReturnResult<T> Insert(T item)
        {
            string outCode = string.Empty;
            string outMsg = string.Empty;
            int result = 0;

            _db.SetQuery(_storeProcedureConfigs.INSERT_SINGLE_STORE_PROCEDURE, CommandType.StoredProcedure)
                .SetParameter("IN_JSON", SqlDbType.NVarChar, JsonConvert.SerializeObject(item), 4000, ParameterDirection.Input)
                .SetParameter("OUT_CODE", SqlDbType.NVarChar, DBNull.Value, 40000, ParameterDirection.Output)
                .SetParameter("OUT_MSG", SqlDbType.NVarChar, DBNull.Value, 40000, ParameterDirection.Output)
                .ExecuteNoneQuery(out result);

            _db.GetOutValue("OUT_CODE", out outCode)
               .GetOutValue("OUT_MSG", out outMsg);

            return new ReturnResult<T>()
            {
                Code = outCode,
                Message = outMsg
            };
        }

        public ReturnResult<T> Insert(List<T> items)
        {
            string outCode = string.Empty;
            string outMsg = string.Empty;
            int result = 0;

            _db.SetQuery(_storeProcedureConfigs.INSERT_LIST_STORE_PROCEDURE, CommandType.StoredProcedure)
                .SetParameter("IN_JSON", SqlDbType.NVarChar, JsonConvert.SerializeObject(items), 4000, ParameterDirection.Input)
                .SetParameter("OUT_CODE", SqlDbType.NVarChar, DBNull.Value, 40000, ParameterDirection.Output)
                .SetParameter("OUT_MSG", SqlDbType.NVarChar, DBNull.Value, 40000, ParameterDirection.Output)
                .ExecuteNoneQuery(out result);

            _db.GetOutValue("OUT_CODE", out outCode)
               .GetOutValue("OUT_MSG", out outMsg);

            return new ReturnResult<T>()
            {
                Code = outCode,
                Message = outMsg
            };
        }

        public ReturnResult<T> Update(T item)
        {
            string outCode = string.Empty;
            string outMsg = string.Empty;
            int result = 0;

            _db.SetQuery(_storeProcedureConfigs.UPDATE_SINGLE_STORE_PROCEDURE, CommandType.StoredProcedure)
                .SetParameter("IN_JSON", SqlDbType.NVarChar, JsonConvert.SerializeObject(item), 4000, ParameterDirection.Input)
                .SetParameter("OUT_CODE", SqlDbType.NVarChar, DBNull.Value, 40000, ParameterDirection.Output)
                .SetParameter("OUT_MSG", SqlDbType.NVarChar, DBNull.Value, 40000, ParameterDirection.Output)
                .ExecuteNoneQuery(out result);

            _db.GetOutValue("OUT_CODE", out outCode)
               .GetOutValue("OUT_MSG", out outMsg);

            return new ReturnResult<T>()
            {
                Code = outCode,
                Message = outMsg
            };
        }

        public ReturnResult<T> Update(List<T> items)
        {
            string outCode = string.Empty;
            string outMsg = string.Empty;
            int result = 0;

            _db.SetQuery(_storeProcedureConfigs.UPDATE_LIST_STORE_PROCEDURE, CommandType.StoredProcedure)
                .SetParameter("IN_JSON", SqlDbType.NVarChar, JsonConvert.SerializeObject(items), 4000, ParameterDirection.Input)
                .SetParameter("OUT_CODE", SqlDbType.NVarChar, DBNull.Value, 40000, ParameterDirection.Output)
                .SetParameter("OUT_MSG", SqlDbType.NVarChar, DBNull.Value, 40000, ParameterDirection.Output)
                .ExecuteNoneQuery(out result);

            _db.GetOutValue("OUT_CODE", out outCode)
               .GetOutValue("OUT_MSG", out outMsg);

            return new ReturnResult<T>()
            {
                Code = outCode,
                Message = outMsg
            };
        }
    }

}
