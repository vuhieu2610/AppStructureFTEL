using AppOutSideAPI.Common;
using AppOutSideAPI.Data.Class;
using AppOutSideAPI.Data.Interfaces;
using EntityData;
using EntityData.Common;
using StructureMap;
using System;
using System.Collections.Generic;

namespace AppOutSideAPI.Business
{
    public class UserBusiness : IDisposable
    {
        //private IUnitOfWork unitOfWork = ObjectFactory.Container.GetInstance<IUnitOfWork>();
        private IUnitOfWork unitOfWork = new UnitOfWork();

        public ReturnResult<Users> GetPaging(BaseCondition condition)
        {
            return unitOfWork.UserRepository.GetPaging(condition);
        }

        public ReturnResult<Users> GetSingle(Users item)
        {
            return unitOfWork.UserRepository.GetSingle(item);
        }

        public ReturnResult<Users> InsertSingle(Users item)
        {
            return unitOfWork.UserRepository.Insert(item);
        }

        public ReturnResult<Users> InsertList(List<Users> items)
        {
            return unitOfWork.UserRepository.Insert(items);
        }

        public ReturnResult<Users> UpdateSingle(Users item)
        {
            return unitOfWork.UserRepository.Update(item);
        }

        public ReturnResult<Users> Delete(Users item)
        {
            return unitOfWork.UserRepository.Delete(item);
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
