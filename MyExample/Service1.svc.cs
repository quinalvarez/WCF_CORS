using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Runtime.InteropServices;

namespace MyExample
{
    public class Service1 : IService1
    {
        [DllImport("CliSiTef32I.dll", CallingConvention = CallingConvention.ThisCall)]
        private static extern int ConfiguraIntSiTefInterativoEX(string SitefIP, string MerchantID,
                                                string TerminalID, string Reservado, string AdditionalParam);
        public ServiceResponse GetData(string value)
        {
            int num = 0;
            try
            {
                num = ConfiguraIntSiTefInterativoEX("52.67.141.229", "ARGISA01", "AR000001", "0", "CUIT=30708547472;CUITISV=30708547472");
                Console.Write(num);
            }
            catch (Exception ex) 
            {
                return new ServiceResponse
                {
                    Value = string.Format("Error {0}", ex.Message)
                };
            }
            return new ServiceResponse
            {
                Value = string.Format("You entered: {0}", num)
            };
        }

    }
}
