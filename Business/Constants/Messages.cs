using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Concrete;
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
        public static string InvalidBrand = "Geçersiz marka adı";
        public static string UserNotFound = "Kullanıcı bulunamadı";
        public static string PasswordError="Şifre hatalı";
        public static string SuccessfulLogin="Sisteme giriş başarılı";
        public static string UserAlreadyExists= "Bu kullanıcı zaten mevcut";
        public static string UserRegistered="Kullanıcı başarıyla kaydedildi";
        public static string AccessTokenCreated="Access Token başarıyla oluşturuldu";
        public static string ProductAdded = "Ürün eklendi";
        public static string ProductNameInvalid = "Ürün ismi geçersiz";
        public static string MaintenanceTime = "Sistem bakımda";
        public static string ProductsListed = "Ürünler listelendi";
        public static string ProductCountOfCategoryError = "Bir kategoride en fazla 10 ürün olabilir";
        public static string ProductNameAlreadyExists = "Bu isimde zaten başka bir ürün var";
        public static string CategoryLimitExceded = "Kategori limiti aşıldığı için yeni ürün eklenemiyor";
        public static string AuthorizationDenied = "Yetkiniz yok.";
    }
}
