using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;

namespace ConsoleUI
{
    class Program
    {

        static void Main(string[] args)
        {


            bool deger = true;
            while (deger)
            {
                deger = AnaMenu();
            }
        }

        private static bool AnaMenu()
        {
            Console.Clear();
            Console.WriteLine("   ---------- Araba Kiralama Servisi ----------");
            Console.WriteLine();


            Console.WriteLine("  Menu");
            Console.WriteLine("  ----------------------");
            Console.WriteLine("  1- Araçları Listele");
            Console.WriteLine("  2- Müşteri Ekle");
            Console.WriteLine("  3- Müşteri Sil");
            Console.WriteLine("  4- Araç Kirala");
            Console.WriteLine("  5- Araç İade");
            Console.WriteLine("  6- Kullanıcı Ekle");
            Console.WriteLine("  7- Kullanıcı Sil");
            Console.WriteLine("  8- Renk Ekle");
            Console.WriteLine("  9- Renk Sil");
            Console.WriteLine("  10- Kiralık araçları listele");
            Console.WriteLine("  11- Çıkış");
            Console.WriteLine("  ----------------------");

            Console.Write(" Seçiminiz : ");
            string istek = Console.ReadLine();

            switch (istek)
            {
                case "1":
                    Listele();
                    break;
                case "2":

                    MusteriEKle();
                    break;
                case "3":
                    MusteriSil();
                    break;
                case "4":
                    AracKirala();
                    break;
                case "5":
                    AracIade();
                    break;
                case "6":
                    KullaniciEkle();
                    break;
                case "7":
                    KullaniciSil();
                    break;
                case "8":
                    RenkEkle();
                    break;
                case "9":
                    RenkSil();
                    break;
                case "10":
                    KiralikListele();
                    break;
                case "11":
                    return false;

                    
                default:
                    Console.WriteLine();
                    Console.WriteLine("Lütfen bir seçim yapınız.. (Devam etmek için Enter Tuşuna basınız)");
                    Console.ReadLine();
                    break;
            }


            return true;
        }

        private static void KiralikListele()
        {
            Console.WriteLine("***************************************************************");
            Console.WriteLine("   Id   |    UserName   |  Brand Name | Rent Date | Return Date");
            Console.WriteLine("***************************************************************");

            RentalManager rentalManager=new RentalManager(new EfRentalDal() );

            var result = rentalManager.GetRentCarDetail();
            if (result.Success)
            {
                foreach (var rental in result.Data)
                {
                    Console.WriteLine(rental.id+" | "+rental.CustomerName +" | "+rental.BrandName +" | "+rental.RentDate+" | "+rental.ReturnDate);
                }

                Console.WriteLine(result.Message);
            }

            AnaMenuyeDon();
        }

        private static void AracIade()
        {
            RentalManager rentalManager=new RentalManager(new EfRentalDal());

            var listele = rentalManager.GetAll();
            foreach (var rent in listele.Data)
            {
                Console.WriteLine(rent.Id+" "+rent.CarId+" "+rent.CustomerId+" "+rent.RentDate+" "+rent.ReturnDate);
            }

            Console.WriteLine("iade etmek istediğiniz id yi giriniz");
            int rentId = Int32.Parse(Console.ReadLine());
            var result2 = rentalManager.ReturnCar(rentId);
            if (result2.Success)
            {
                Console.WriteLine(result2.Message);
            }
            else
            {
                Console.WriteLine(result2.Message);
            }
            AnaMenuyeDon();
        }

        private static void AracKirala()
        {
            CarManager carManager=new CarManager(new EfCarDal() );
            RentalManager rentalManager = new RentalManager(new EfRentalDal());

            var result = carManager.GetCarDetails();
            foreach (var car in result.Data)
            {
                Console.WriteLine(car.Id+" "+car.CarName+" "+car.Color+" "+car.ModelYear+" "+car.DailyPrice+" "+car.Description);
            }

            Console.WriteLine("Kiralamak istediğiniz aracın id sini giriniz");
            int carId=Int32.Parse(Console.ReadLine());
            Console.WriteLine("Kullanici id nizi giriniz");
            int userId=Int32.Parse(Console.ReadLine());
            var result2 = rentalManager.RentACar(carId, userId);
            if (result2.Success)
            {
                Console.WriteLine(result2.Message);
            }
            else
            {
                Console.WriteLine(result2.Message);
            }
            AnaMenuyeDon();
        }

        private static void RenkSil()
        {
            ColorManager colorManager = new ColorManager(new EfColorDal());
            var result = colorManager.GetAll();
            foreach (var color in result.Data)
            {
                Console.WriteLine(color.Id+" - "+color.Name);
            }

            Console.WriteLine("Silmek istediğiniz rengin idsini giriniz");
            int colorId=Int32.Parse(Console.ReadLine());

            colorManager.Delete(new Color{Id = colorId});
            AnaMenuyeDon();
        }

        private static void RenkEkle()
        {
            ColorManager colorManager = new ColorManager(new EfColorDal());
            Console.WriteLine("Renk Giriniz");
            string colorName = Console.ReadLine();
            colorManager.Add(new Color{Name = colorName});
            AnaMenuyeDon();
        }

        private static void KullaniciSil()
        {
            UserManager userManager = new UserManager(new EfUserDal());
            var kullaniciListele = userManager.GetAll();
            foreach (var user in kullaniciListele.Data)
            {
                Console.WriteLine(" Id    |  First Name  |  Last Name  |  Email");
                Console.WriteLine(user.Id+" "+user.FirstName +" "+user.LastName+" "+user.Email);
            }

            Console.WriteLine("Silmek istediğiniz kullanıcının idsini giriniz");

            var result = userManager.Delete(new User {Id = Int32.Parse(Console.ReadLine())});
            if (result.Success)
            {
                Console.WriteLine(result.Message);
            }
            else
            {
                Console.WriteLine(result.Message);
            }
            AnaMenuyeDon();
        }

        private static void KullaniciEkle()
        {
            UserManager userManager = new UserManager(new EfUserDal());
            Console.WriteLine("Adınız");
            string ad = Console.ReadLine();
            Console.WriteLine("Soyadınız");
            string soyad = Console.ReadLine();
            Console.WriteLine("email adresiniz");
            string email = Console.ReadLine();
            Console.WriteLine("Sifreniz");
            string sifre = Console.ReadLine();

            var result = userManager.Add(new User {FirstName = ad,LastName =soyad,Email = email,Password = sifre});

            if (result.Success)
            {
                Console.WriteLine(result.Message);
            }
            else
            {
                Console.WriteLine(result.Message);
            }
            AnaMenuyeDon();
        }

 

        private static void MusteriSil()
        {
            CustomerManager customerManager = new CustomerManager(new EfCustomerDal());
            CustomerBaslik();
            var result1 = customerManager.GetAll();
            foreach (var customer in result1.Data)
            {
                Console.WriteLine(customer.Id + " | " + customer.UserId + " | " + customer.CompanyName);
            }
            Console.WriteLine(result1.Message);
            Console.WriteLine("**************************");
            Console.WriteLine("Silmek istediğiniz müşterinin id sini giriniz");
            int customerId=Int32.Parse(Console.ReadLine());
            
            var result2 = customerManager.Delete(new Customer{Id = customerId});
            if (result2.Success)
            {
                Console.WriteLine(result2.Message);
            }

            else
            {
                Console.WriteLine(result2.Message);
            }
     
            AnaMenuyeDon();
        }

        private static void MusteriEKle()
        {
            CustomerManager customerManager = new CustomerManager(new EfCustomerDal());
            Console.WriteLine("Kullanıcı id sini giriniz");
            int userId =Int32.Parse(Console.ReadLine()); 
            Console.WriteLine("Şirket adını giriniz");
            string companyName = Console.ReadLine();

            var result= customerManager.Add(new Customer
            {
                UserId = userId,
                CompanyName = companyName
            });
            if (result.Success)
            {
                Console.WriteLine(result.Message);
                CustomerBaslik();
                var result1 = customerManager.GetAll();
                foreach (var customer in result1.Data)
                {
                    Console.WriteLine(customer.Id+" | "+ customer.UserId+" | "+customer.CompanyName);
                }

                Console.WriteLine(result1.Message);
            }
            else
            {
                Console.WriteLine(result.Message);
            }

            AnaMenuyeDon();
        }

        private static void CustomerBaslik()
        {
            Console.WriteLine("*************************************");
            Console.WriteLine("   Id   |    UserId   |  Company Name");
            Console.WriteLine("*************************************");
        }

        private static void Listele()
        {
            BaslikCarDetail();
            CarManager carManager = new CarManager(new EfCarDal());
            var result = carManager.GetCarDetails();
            if (result.Success)
            {
                foreach (var carDetailDto in result.Data)
                {
                    Console.WriteLine(carDetailDto.Id+" = "+carDetailDto.CarName+" - "+carDetailDto.Color+" - "+carDetailDto.ModelYear
                    +" - "+carDetailDto.DailyPrice+" - "+carDetailDto.Description);
                }

                Console.WriteLine(result.Message);
            }
            else
            {
                Console.WriteLine(result.Message);
            }
            
            AnaMenuyeDon();
        }

        private static void AnaMenuyeDon()
        {
            Console.WriteLine();
            Console.WriteLine("Ana Menü için Enter tuşuna basınız...");
            Console.ReadLine();
        }
        private static void BaslikCarDetail()
        {
            Console.WriteLine("  ID  | Brand | Color | Daily Price  | Model Year | Description");
            Console.WriteLine("---------------------------------------------------------------");

        }

       


      

    

      



       



    }
}
