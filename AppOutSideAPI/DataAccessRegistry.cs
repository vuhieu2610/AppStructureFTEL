using AppOutSideAPI.Data.Class;
using AppOutSideAPI.Data.Interfaces;
using EntityData;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppOutSideAPI
{
    public class DataAccessRegistry : Registry
    {
        public DataAccessRegistry()
        {
            For<IUnitOfWork>().Use<UnitOfWork>().Transient().Ctor<bool>("useTransaction").Is(true);
            For<IGenericRepository<Users>>().Use<GenericRepository<Users>>();

        }
    }
}
