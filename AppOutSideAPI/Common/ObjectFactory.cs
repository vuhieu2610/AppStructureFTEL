//using StructureMap;
//using System;
//using System.Threading;

//namespace AppOutSideAPI.Common
//{
//    public static class ObjectFactory
//    {
//        private static Container _container = null;

//        public static IContainer Container
//        {
//            get
//            {
//                if (_container == null)
//                {
//                    _container = new Container(config =>
//                    {
//                        config.AddRegistry(new DataAccessRegistry());
//                    });
//                }

//                return _container;
//            }
//        }
//    }
//}
