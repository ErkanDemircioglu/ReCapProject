using System;
using System.Collections.Generic;
using System.Text;
using Entities.Concrete;

namespace Business.Constants
{
    public static class Messages
    {
        public static string Added = "Eklendi";
        public static string LowPrice = "Günlük fiyat sıfır ve altı olamaz";

        public static string Updated = "Güncellendi";
        public static string Deleted = "Listeden silindi";

        public static string MaintenanceDay = "Bakımda";
        public static string List = "Listelendi";
        public static string OutOfHours = "Mesai dışı";
        internal static string UserNameMessage = "Kullanıcı adı üç karakterden az olamaz";
        internal static string ExistUser = "Geçerli bir kullanıcı giriniz";
        internal static string InvalidCar = "Bu araç kiralanamaz";
        internal static string RentACar = "Arac kiralandı";

        public static string InvalidUser = "UserId sıfır ya da boş değer içermemeli";
        public static string NotRentCar = "Bu aracı kiralayamazsınız.";
    }
}
