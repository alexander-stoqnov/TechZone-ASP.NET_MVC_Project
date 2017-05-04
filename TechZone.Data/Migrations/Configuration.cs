namespace TechZone.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models.EntityModels;
    using Models.Enums;

    internal sealed class Configuration : DbMigrationsConfiguration<TechZoneContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(TechZoneContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            SeedRoles(roleManager);
            SeedUsers(context, userManager);
            SeedGraphicCards(context);
            SeedHardDrives(context);
            SeedProcessors(context);
            context.SaveChanges();
        }

        private void SeedProcessors(TechZoneContext context)
        {
            context.Processors.AddOrUpdate(p => p.Name, new Processor
            {
                Brand = ProcessorBrandType.Intel,
                Series = ProcessorSeriesType.i7,
                Cores = ProcessorCoresType.Quad_Core,
                Cache = 8,
                ProcessorSpeed = 4.2m,
                Price = 339.99m,
                Discount = 5,
                Quantity = 232,
                Name = "Intel 7th Gen Intel i7-7700K",
                Description = "Leading the pack is the Intel Core i7-7700K processor. Architected for performance, this processor packs 4 high performing cores with core base frequency of 4.2GHz and 8MB of cache memory. Kick up the performance even higher with Intel Turbo Boost 2.0 technology to bump the max turbo frequency to an amazing 4.5GHz. Add Intel Hyper-Threading Technology for 8-way multitasking to deliver the performance knockout punch. Not enough? For the enthusiast, this processor is unlocked, you can tweak the performance to its fullest potential.",
                ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/51zkoInLSiL.jpg",
                Guarantee = GuaranteeDurationType.Thirty_Six_Months
            }, new Processor
            {
                Brand = ProcessorBrandType.Intel,
                Series = ProcessorSeriesType.i7,
                Cores = ProcessorCoresType.Quad_Core,
                Cache = 8,
                ProcessorSpeed = 4m,
                Price = 339.99m,
                Discount = 15,
                Quantity = 65,
                Name = "Intel Core i7 6700K 4.00 GHz, Skylake, Socket LGA 1151",
                Description = "With faster, intelligent, multi-core technology that applies processing power where it's needed most, Intel Core i7 processors deliver an incredible breakthrough in PC performance. You'll multitask applications faster and unleash incredible digital media creation. And you'll experience maximum performance for everything you do.",
                ImageUrl = "http://saletookar.com/oc-content/uploads/0/53.jpg",
                Guarantee = GuaranteeDurationType.Sixty_Months
            }, new Processor
            {
                Brand = ProcessorBrandType.Intel,
                Series = ProcessorSeriesType.i7,
                Cores = ProcessorCoresType.Quad_Core,
                Cache = 8,
                ProcessorSpeed = 4m,
                Price = 311.00m,
                Quantity = 89,
                Name = "Intel Core i7-4790K Processor (8M Cache, up to 4.40 GHz)",
                Description = "The Intel 4th generation Core i7-4790 processor is based on the new 22nm Haswell Microarchitecture for improved CPU performance. Advanced power management innovations help keep power consumption in check. New compute instructions ensure enhanced performance per cycle. Improved Intel integrated graphics enables discrete-level graphics performance. Extract more power from your Haswell core-based processor.",
                ImageUrl = "https://n2.sdlcdn.com/imgs/a/l/r/Intel-Core-i7-4790-Processor-SDL147336105-2-4b8bb.jpg",
                Guarantee = GuaranteeDurationType.Twelve_Months
            }, new Processor
            {
                Brand = ProcessorBrandType.Intel,
                Series = ProcessorSeriesType.i5,
                Cores = ProcessorCoresType.Quad_Core,
                Cache = 6,
                ProcessorSpeed = 3.8m,
                Price = 311.00m,
                Discount = 10,
                Quantity = 429,
                Name = "Intel Core i5-7600K LGA 1151",
                Description = "With new 7th Generation Intel® Core™ processors, your PC will meet every demand quickly and seamlessly – get 0.5 second wake with Windows* Modern Standby and Intel® Ready Mode Technology (Intel® RMT), and rapidly switch between applications and web pages with Intel® Speed Shift Technology.",
                ImageUrl = "https://images10.newegg.com/ProductImage/19-117-728-Z01.jpg",
                Guarantee = GuaranteeDurationType.Twenty_Four_Months
            }, new Processor
            {
                Brand = ProcessorBrandType.Intel,
                Series = ProcessorSeriesType.i5,
                Cores = ProcessorCoresType.Quad_Core,
                Cache = 6,
                ProcessorSpeed = 3.2m,
                Price = 197.03m,
                Quantity = 321,
                Name = "Intel Core i5 6500 3.20 GHz Quad Core, LGA 1151, 6MB Cache",
                Description = "With intelligent performance that accelerates in response to demanding tasks, such as playing games and editing photos, the Intel Core i5 processor moves faster when you do. The Intel Core i5 processor automatically allocates processing power where it's needed most. Whether you're creating HD video, composing digital music, editing photos, or playing the coolest PC games - with the Intel Core i5 processor you can multitask with ease and be more productive than ever.",
                ImageUrl = "https://images10.newegg.com/NeweggImage/ProductImage/19-117-563-01.jpg",
                Guarantee = GuaranteeDurationType.Twenty_Four_Months
            }, new Processor
            {
                Brand = ProcessorBrandType.Intel,
                Series = ProcessorSeriesType.i5,
                Cores = ProcessorCoresType.Quad_Core,
                Cache = 6,
                ProcessorSpeed = 3m,
                Price = 199.89m,
                Quantity = 43,
                Discount = 20,
                Name = "Intel Core i5-7400 Kaby Lake Quad-Core 3.0 GHz LGA 1151",
                Description = "Are you amazed by the things you can do with your computer? . If you bought your computer more than a handful of years ago, you're missing more than you know - uncompromised gaming, while you stream, chat and share with your community - stunning visuals of 4K HDR premium content - transport into a great VR experience. With premium performance and new & enhanced features, a desktop computer based on an 7th Gen Intel Core processor is always ready for real-life productivity, creativity and entertainment. With a range of smart, stylish designs and sizes, there is a 7th Gen Intel Core Desktop powered computer to fit a wide range of budgets and needs.",
                ImageUrl = "https://images10.newegg.com/ProductImage/19-117-731-Z01.jpg",
                Guarantee = GuaranteeDurationType.Thirty_Six_Months
            }, new Processor
            {
                Brand = ProcessorBrandType.Intel,
                Series = ProcessorSeriesType.i3,
                Cores = ProcessorCoresType.Dual_Core,
                Cache = 3,
                ProcessorSpeed = 3.70m,
                Price = 115.98m,
                Quantity = 854,
                Name = "Intel 3.70 GHz Core i3-6100 3M Cache",
                Description = "The 6th generation Intel Core processor is based on the Skylake micro architecture and built with 14nm manufacturing process. It comes packed with advanced features to take your productivity, creativity and 3D gaming to the next level. And, by enabling new exciting Windows 10 features, the 6th generation Intel Core processor empowers you to unleash your imagination and explore the possibilities. The Intel Core i3-6100 comes with two-core, four-thread configuration, delivering the performance and capability you need for everyday home and office tasks, as well as a host of new features to help you make the most of your PC experience.",
                ImageUrl = "https://images10.newegg.com/NeweggImage/productimage/2MN-0004-00002-01.jpg",
                Guarantee = GuaranteeDurationType.Twenty_Four_Months
            }, new Processor
            {
                Brand = ProcessorBrandType.Intel,
                Series = ProcessorSeriesType.i3,
                Cores = ProcessorCoresType.Dual_Core,
                Cache = 3,
                ProcessorSpeed = 3.90m,
                Price = 115.09m,
                Quantity = 291,
                Name = "Intel Core i3-7100 7th Gen, 3M Cache,3.90 GHz",
                Description = "Unprecedented power and responsiveness, paired with easy, built-in security, means you can work, play, and create as quickly and seamlessly as your heart desires. Plus, by enabling superior 4K resolution, 7th Generation Intel® Core™ processors will have you feeling immersed in gaming and entertainment like never before, whether at home or on the go.",
                ImageUrl = "https://images10.newegg.com/ProductImage/19-117-734-Z01.jpg",
                Guarantee = GuaranteeDurationType.Twelve_Months
            }, new Processor
            {
                Brand = ProcessorBrandType.Intel,
                Series = ProcessorSeriesType.i3,
                Cores = ProcessorCoresType.Dual_Core,
                Cache = 4,
                ProcessorSpeed = 4m,
                Price = 149.99m,
                Quantity = 64,
                Name = "Intel Core i3-7300 Kaby Lake, Dual-Core, 4.0 GHz",
                Description = "The Intel Core i3-7300, developed under the codename Kaby Lake-S, is a desktop processor that was first available for purchase in January 2017. It is a dual-core CPU, resulting in a lower multi-tasking potential when compared to processors with more cores. This Core i3 series CPU operates at a stock clock speed of 4.00 GHz1.10 GHz faster than an average desktop processor. Its cache size is 4MB, which is an average cache among desktop processors, and its 64GB of maximum supported memory is above average compared to all desktop CPUs.",
                ImageUrl = "https://photos05.redcart.pl/templates/images/thumb/11998/432/600/pl/0/templates/images/products/11998/1-PROINTCI30069_0.jpg",
                Guarantee = GuaranteeDurationType.Sixty_Months
            }, new Processor
            {
                Brand = ProcessorBrandType.AMD,
                Series = ProcessorSeriesType.FX,
                Cores = ProcessorCoresType.Octa_Core,
                Cache = 8,
                ProcessorSpeed = 4m,
                Price = 119.99m,
                Discount = 24,
                Quantity = 154,
                Name = "AMD FX-8350 FX-Series 8-Core",
                Description = "AMD FX 8-Core Processor, Unlocked, Black Edition. AMD's next-generation architecture takes 8-core processing to a new level. Get up to 24% better frame rates in some of the most demanding games, at stunning resolutions. Get faster audio encoding so you can enjoy your music sooner. Go up to 5.0 GHz with aggressive cooling solutions from AMD. What Is In The Box: CPU, Heat Sink and Fan(E3), Thermal Paste is already applied on the Heat Sink, FX Bezel Sticker.",
                ImageUrl = "https://p1.akcdn.net/full/148865399.amd-x8-fx-8350-4ghz-am3.jpg",
                Guarantee = GuaranteeDurationType.Thirty_Six_Months
            }, new Processor
            {
                Brand = ProcessorBrandType.AMD,
                Series = ProcessorSeriesType.FX,
                Cores = ProcessorCoresType.Octa_Core,
                Cache = 8,
                ProcessorSpeed = 4.4m,
                Price = 168.88m,
                Quantity = 84,
                Name = "AMD FX-9370 Vishera 8-Core 4.4 GHz",
                Description = "The AMD FX-9370, developed under the codename Zambezi, is a desktop processor that was first available for purchase in June 2013. It has an octa-core model, resulting in a high multi-tasking potential.This FX series CPU operates at a stock clock speed of 4.70 GHzone of the fastest desktop processors.If that isn't enough power, the FX-9370 has been tested by AMD to handle a maximum overclocked speed up to 5.00 GHz.Its cache size is 8MB, which is an above average cache among desktop processors.",
                ImageUrl = "https://images10.newegg.com/NeweggImage/ProductImage/19-113-346-02.jpg",
                Guarantee = GuaranteeDurationType.Twenty_Four_Months
            }, new Processor
            {
                Brand = ProcessorBrandType.AMD,
                Series = ProcessorSeriesType.Ryzen,
                Cores = ProcessorCoresType.Octa_Core,
                Cache = 16,
                ProcessorSpeed = 3.7m,
                Price = 325.70m,
                Quantity = 182,
                Discount = 5,
                Name = "AMD Ryzen 7 1700 Processor with Wraith Spire LED Cooler",
                Description = "AMD's Ryzen1700 combines 8 processor cores and 16 threads with a surprisingly low 65W TDP to deliver an efficient, powerful processing solution like no other. Boasting AMD SenseMI technology with true machine intelligence, the Ryzen 7 1700 also comes equipped with the AMD Wraith Spire cooler, featuring color-configurable LED illumination for customized style. REQUIRES A DISCRETE GRAPHICS CARD, NOT INCLUDED.",
                ImageUrl = "https://www.evetech.co.za/repository/ProductImages/amd-ryzen-7-1700-processor-1000px-v2.jpg",
                Guarantee = GuaranteeDurationType.Thirty_Six_Months
            }, new Processor
            {
                Brand = ProcessorBrandType.AMD,
                Series = ProcessorSeriesType.Ryzen,
                Cores = ProcessorCoresType.Quad_Core,
                Cache = 16,
                ProcessorSpeed = 3.5m,
                Price = 189.99m,
                Quantity = 33,
                Name = "AMD Ryzen 5 1500X Processor with Wraith Spire Cooler",
                Description = "The Ryzen 5 1500X has the potential to be a disruptive product for AMD, one that wrecks through Intel's sub-$200 CPU lineup, and we are relieved to report that it did succeed to an extent. Suddenly, Intel's sub-$200 processors, including the Core i3-7100 dual-core, Core i5-7400 quad-core, and $206 Core i5-7500, seem like bad options. Intel is giving you too little for the money. Intel's \"Kaby Lake\" architecture continues to maintain per-core performance leadership over AMD's \"Zen,\" but not by much thanks to the latter's huge leap in core performance over previous AMD chips. The Ryzen 5 1500X processor also gives you so much more for its $189 price - 4 cores with SMT enabling 8 threads (competing Core i5 chips lack HyperThreading), higher clock speeds out of the box, and unlocked base-clock multipliers. Intel has the gall to ask $189 for the dual-core i3-7350K with unlocked multiplier and half the L3 cache.",
                ImageUrl = "https://www.amd.com/en/system/files/11157-ryzen-5-pib-left-facing-1260x709.png",
                Guarantee = GuaranteeDurationType.Sixty_Months
            }, new Processor
            {
                Brand = ProcessorBrandType.AMD,
                Series = ProcessorSeriesType.Ryzen,
                Cores = ProcessorCoresType.Octa_Core,
                Cache = 16,
                ProcessorSpeed = 3.6m,
                Price = 469.99m,
                Quantity = 152,
                Name = "AMD Ryzen 7 1800X",
                Description = "AMD’s high-performance x86 Core “Zen” architecture delivers >52% improvement in instructions-per-clock cycle over the previous generation AMD core, without increasing power. AMD introduces SenseMI technology, a set of learning and adapting features that help the AMD Ryzen™ processor customize its performance to you and your applications. Finally: performance that thinks. The new AMD AM4 Platform puts effortless compatibility front and center. Our new 1331-pin processor socket works with the 7th Gen AMD APU, AMD Ryzen CPU, and the upcoming \"Raven Ridge\" APU. The one Socket AM4 motherboard you buy will work with any AM4 processor! And with support for the latest I/O standards like USB 3.1 Gen 2, NVMe, or PCI Express® 3.0, it's easy to build a high-performance system that can grow with your needs.",
                ImageUrl = "https://images10.newegg.com/NeweggImage/ProductImage/19-113-430-S01.jpg",
                Guarantee = GuaranteeDurationType.Thirty_Six_Months
            }, new Processor
            {
                Brand = ProcessorBrandType.AMD,
                Series = ProcessorSeriesType.A,
                Cores = ProcessorCoresType.Quad_Core,
                Cache = 4,
                ProcessorSpeed = 3.7m,
                Price = 129.46m,
                Quantity = 152,
                Name = "AMD A10-Series APU A10-7850K",
                Description = "This new APU form AMD continues to revolutionize the microprocessor market. It is the first APU to deliver 856Glops and the first with H.S.A. (Heterogeneous Systems Architecture which defines how the 4 compute cores interact with the 8 graphics cores. This APU also is Mantle capable, improving it's console-like optimizations.",
                ImageUrl = "https://img.pccomponentes.com/articles/6/62664/amd-a10-7850k-3-7ghz.jpg",
                Guarantee = GuaranteeDurationType.Twelve_Months
            }, new Processor
            {
                Brand = ProcessorBrandType.AMD,
                Series = ProcessorSeriesType.A,
                Cores = ProcessorCoresType.Dual_Core,
                Cache = 4,
                ProcessorSpeed = 3.9m,
                Price = 48.49m,
                Quantity = 231,
                Discount = 7,
                Name = "AMD A6-6400K Richland 3.9GHz Dual-Core",
                Description = "AMD A6-64000K Dual-Core APU Processor 3.9GHz Socket FM2, Retail (Black Edition)",
                ImageUrl = "http://www.pdf-manuals.com/p/pictures21/amd-a6-6400k-dual-core-a6-series-accelerated-ad640kokhlbox-b-h-243553.jpg",
                Guarantee = GuaranteeDurationType.Twenty_Four_Months
            });
        }

        private void SeedHardDrives(TechZoneContext context)
        {
            context.HardDrives.AddOrUpdate(h => h.Name, new HardDrive
            {
                DriveBrand = HardDriveBrandType.Seagate,
                DriveType = HardDriveType.HDD,
                Capacity = 1000,
                Price = 53.99m,
                Discount = 13,
                Quantity = 550,
                Name = "Seagate Expansion, 1TB Portable External Hard Drive, USB 3.0",
                Description = "The Seagate Expansion portable drive provides extra storage for your ever-growing collection of files. Instantly add space for more files, consolidate all of your files to a single location, or free up space on your computer's internal drive to help improve performance. Setup is straightforward; simply plug in the included USB cable, and you are ready to go. The drive is automatically recognized by the Windows operating system, so there is no software to install or configure. Saving files is easy too–just drag-and-drop. Take advantage of the fast data transfer speeds with the USB 3.0 interface by connecting to a SuperSpeed USB 3.0 port. USB 3.0 is backwards compatible with USB 2.0 for additional system compatibility. System Requirements: Windows 7 or higher, SuperSpeed USB 3.0 port (required for USB 3.0 transfer speeds or backwards compatible with USB 2.0 ports at USB 2.0 transfer speeds, compatibility may vary depending on the user's hardware configuration and operating system). Box Contents: Seagate Expansion Drive, 18-inch USB 3.0 Cable, and Quick Start Guide.",
                ImageUrl = "http://s.productreview.com.au/products/images/d850c9c0-04a8-4d94-8fd5-14e8a2ae28b1.jpeg",
                Guarantee = GuaranteeDurationType.Twelve_Months
            }, new HardDrive
            {
                DriveBrand = HardDriveBrandType.WesternDigital,
                DriveType = HardDriveType.HDD,
                Capacity = 2000,
                Price = 76.40m,
                Quantity = 350,
                Name = "WD 2TB Elements Portable External Hard Drive - USB 3.0",
                Description = "WD Elements portable hard drives with USB 3.0 offer reliable, high-capacity storage to go, ultra-fast data transfer rates and universal connectivity with USB 2.0 and USB 3.0 devices. The small, lightweight enclosure features massive capacity and WD quality and reliability. It includes a free trial of WD SmartWare Pro backup software for local and cloud backup.",
                ImageUrl = "https://images10.newegg.com/ProductImage/22-236-519-05.jpg",
                Guarantee = GuaranteeDurationType.Twenty_Four_Months
            }, new HardDrive
            {
                DriveBrand = HardDriveBrandType.Seagate,
                DriveType = HardDriveType.HDD,
                Capacity = 5000,
                Price = 119.99m,
                Quantity = 120,
                Name = "Seagate Expansion 5TB Desktop External Hard Drive USB 3.0",
                Description = "The Seagate Expansion desktop drive provides extra storage for your ever-growing collection of files. Instantly add space for more files, consolidate all of your files to a single location, or free up space on your computer's internal drive to help improve performance. Setup is straightforward; simply plug in the included power supply and USB cable, and you are ready to go. It is automatically recognized by the Windows operating system, so there is no software to install and nothing to configure. Saving files is easy too-just drag-and-drop. Take advantage of the fast data transfer speeds with the USB 3.0 interface by connecting to a SuperSpeed USB 3.0 port. USB 3.0 is backwards compatible with USB 2.0 for additional system compatibility. System Requirements: Windows 7 or higher, SuperSpeed USB 3.0 port (required for USB 3.0 transfer speeds or backwards compatible with USB 2.0 ports at USB 2.0 transfer speeds, compatibility may vary depending on the user's hardware configuration and operating system). Box Contents: Seagate Expansion Drive, 4-foot (120cm) USB 3.0 Cable, Power adapter and Quick Start Guide.",
                ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/617Fg8dJCsL._SL1267_.jpg",
                Guarantee = GuaranteeDurationType.Twenty_Four_Months
            }, new HardDrive
            {
                DriveBrand = HardDriveBrandType.WesternDigital,
                DriveType = HardDriveType.HDD,
                Capacity = 1000,
                Price = 49.99m,
                Quantity = 430,
                Name = "WD Purple 1TB Surveillance Hard Drive, 5400 RPM, 6 Gb/s",
                Description = "WD Purple surveillance storage is built to handle up to 32 HD cameras per drive and is designed for 24/7, always on, high-definition surveillance security systems that use up to eight hard drives.",
                ImageUrl = "http://www.ctiai.com/store/media/catalog/product/cache/2/image/800x800/9df78eab33525d08d6e5fb8d27136e95/1/t/1tb.png",
                Guarantee = GuaranteeDurationType.Twenty_Four_Months
            }, new HardDrive
            {
                DriveBrand = HardDriveBrandType.Toshiba,
                DriveType = HardDriveType.HDD,
                Capacity = 500,
                Price = 36.50m,
                Discount = 18,
                Quantity = 250,
                Name = "Toshiba 3.5-Inch, 500GB, 7200 RPM SATA3, 6.0 GB/s",
                Description = "Toshiba DT01ACA050 500GB 7200RPM SATA3/SATA 6.0 GB/s 32MB Hard Drive (3.5 inch)",
                ImageUrl = "http://compareindia.news18.com/media/gallery/images/2012/feb/mk5065gsx_2_231114491599.jpg",
                Guarantee = GuaranteeDurationType.Twelve_Months
            }, new HardDrive
            {
                DriveBrand = HardDriveBrandType.Seagate,
                DriveType = HardDriveType.HDD,
                Capacity = 2000,
                Price = 66.99m,
                Quantity = 99,
                Name = "Seagate 2TB BarraCuda SATA 6Gb/s",
                Description = "Versatile and dependable, the fierce Seagate Barracuda drives build upon a reliable drive family spanning 20 years. Count on affordable Barracuda drives as 2.5 and 3.5 inch HDD solutions for nearly any application-working, playing and storing your movies and music.",
                ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/51oSffVPIWL._SY355_.jpg",
                Guarantee = GuaranteeDurationType.Thirty_Six_Months
            }, new HardDrive
            {
                DriveBrand = HardDriveBrandType.WesternDigital,
                DriveType = HardDriveType.HDD,
                Capacity = 500,
                Price = 58.45m,
                Quantity = 33,
                Name = "WD Black 500GB, 7200 RPM, 6 Gb/s",
                Description = "WD Black hard drives are designed for enthusiasts and creative professionals looking for leading-edge performance. These 2.5-inch mobile drives are perfect for high-performance applications like photo and video editing, gaming and power PCs. Supported by industry leading 5 year limited warranty.",
                ImageUrl = "http://www.stuartconnections.com/9013-thickbox/Western-Digital-Scorpio-Black-500GB-7200RPM-SATA-II-Laptop-Notebook-Hard-Drive-WD5000BPKT.jpg",
                Guarantee = GuaranteeDurationType.Twenty_Four_Months
            }, new HardDrive
            {
                DriveBrand = HardDriveBrandType.Samsung,
                DriveType = HardDriveType.SSD,
                Capacity = 250,
                Price = 109.99m,
                Quantity = 266,
                Discount = 10,
                Name = "Samsung 850 EVO 250GB SSD",
                Description = "Samsung's 850 EVO series SSD is the industry's #1 best-selling* SSD and is perfect for everyday computing. Powered by Samsung's V-NAND technology, the 850 EVO transforms the everyday computing experience with optimized performance and endurance. Designed to fit desktop PCs, laptops, and ultrabooks, the 850 EVO comes in a wide range of capacities and form factors. *Based on 2015 NPD reported revenue in the US.",
                ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/71gkS5vep8L._SL1500_.jpg",
                Guarantee = GuaranteeDurationType.Twelve_Months
            }, new HardDrive
            {
                DriveBrand = HardDriveBrandType.Samsung,
                DriveType = HardDriveType.SSD,
                Capacity = 500,
                Price = 177.92m,
                Quantity = 121,
                Name = "Samsung 850 EVO 500GB SSD",
                Description = "Upgrading your PC with a Samsung SSD is the most economical way to breathe new life into an aging PC. The 850 EVO reads, writes and multi-tasks at incredible speeds, enhancing boot-up speed, application loading and multi-tasking performance. It's more than an upgrade, it's a complete transformation of your PC.",
                ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/91ZPflI8tzL._SX466_.jpg",
                Guarantee = GuaranteeDurationType.Sixty_Months
            }, new HardDrive
            {
                DriveBrand = HardDriveBrandType.Kingston,
                DriveType = HardDriveType.SSD,
                Capacity = 120,
                Price = 52,
                Quantity = 54,
                Name = "Kingston Digital, 120GB, SSDNow UV400",
                Description = "Kingston's SSDnow UV400 is powered by a four-channel Marvell controller for incredible speeds and higher performance compared to a mechanical hard drive. It dramatically improves the responsiveness of your existing system and is 10 times faster than a 7200RPM hard drive (based on “out-of-box performance” using a SATA Rev. 3.0 motherboard. Speed may vary due to host hardware, software, and usage). Rugged and more reliable and durable than a hard drive, UV400 is built using flash memory so it's shock- and vibration-resistant and less likely to fail than a mechanical hard drive. Its ruggedness makes it ideal for notebooks and other mobile computing devices. UV400 is available in multiple capacities, giving you plenty of space for all your files, applications, videos, photos and other important documents. It's the ideal hard drive Replacement and can also replace a smaller SSD in your system to give you all the room you need.",
                ImageUrl = "https://images10.newegg.com/ProductImage/20-242-257-01.jpg",
                Guarantee = GuaranteeDurationType.Twelve_Months
            }, new HardDrive
            {
                DriveBrand = HardDriveBrandType.Kingston,
                DriveType = HardDriveType.SSD,
                Capacity = 240,
                Price = 109.99m,
                Quantity = 103,
                Name = "Kingston Digital HyperX FURY 240GB ",
                Description = "HyperX® FURY solid-state drive delivers impressive performance at an affordable price, to get you into the game faster and improve your gameplay. Ideal for desktops and notebooks, it features a slim 7mm form factor and SandForce® SF-2281 controller with SATA Rev 3.0 (6Gb/s) performance. The result is faster system boot up, application loading and file execution plus faster map and level load time. Its synchronous NAND allows for higher and consistent drive performance. So you're not only in the game, you're winning it. In 120GB and 240GB capacities, HyperX FURY SSD is available as a stand-alone drive and is easy to install. This cost-efficient upgrade is less disruptive than buying a new system to increase performance. The new FURY entry-level product line from HyperX includes memory and SSDs and is designed for gamers, enthusiasts and system Integrators, who can now have consistent HyperX branding for their gaming PCs. Built with Flash memory, HyperX FURY SSD has no moving parts and is cooler, quieter and more shock- and vibration-resistant than traditional hard drives, making it the ideal hard drive replacement. It's backed by a three-year warranty and free technical support. / Kingston Hyperx FURY 240 GB 2.5\" Internal Solid State Drive - SATA - 500 MB / s Maximum Read Transfer Rate - 500 MB / s Maximum Write Transfer Rate - 22500IOPS Random 4KB Read - 41000IOPS Random 4KB Write / SATA - 500 MB / s Maximum Read Transfer Rate - 500 MB / s Maximum Write Transfer Rate - 22500IOPS Random 4KB Read - 41000IOPS Random 4KB Write.",
                ImageUrl = "http://images.hardwarecanucks.com/image/akg/Storage/Fury/top_sm.jpg",
                Guarantee = GuaranteeDurationType.Sixty_Months
            }, new HardDrive
            {
                DriveBrand = HardDriveBrandType.SanDisk,
                DriveType = HardDriveType.SSD,
                Capacity = 240,
                Price = 93.99m,
                Quantity = 11,
                Name = "Sandisk Z400s 256GB",
                Description = "The Sandisk z400s SSD delivers the performance, capacities, and form factors ideal for replacing HDDs in embedded and mainstream computing platforms. It can outperform HDD by a factor of 20, while providing 5x the reliability at 1/20th the power consumption.",
                ImageUrl = "https://images10.newegg.com/NeweggImage/ProductImage/A85V_1_20160118617953317.jpg",
                Guarantee = GuaranteeDurationType.Twenty_Four_Months
            }, new HardDrive
            {
                DriveBrand = HardDriveBrandType.WesternDigital,
                DriveType = HardDriveType.SSD,
                Capacity = 240,
                Price = 84.99m,
                Quantity = 44,
                Name = "WD Blue 250GB, SSD, SATA 6Gb/s",
                Description = "With superior performance and a leader in reliability, the WD Blue SSD offers digital storage that is optimized for multi-tasking and ready to keep up with your high performance computing needs. Available in both 2.5\" / 7mm cased and M.2 2280 form factors and WD's Functional Integrity Testing Lab (FIT Lab), the WD Blue SSD is compatible with a wide range of computers, so you can be sure you are making the right choice. Combined with the free, downloadable WD SSD Dashboard and a 3-year limited warranty, you can confidently upgrade your system to the WD Blue SSD.",
                ImageUrl = "http://i.expansys.com/i/b/b296045-1.jpg",
                Guarantee = GuaranteeDurationType.Twelve_Months
            }, new HardDrive
            {
                DriveBrand = HardDriveBrandType.SanDisk,
                DriveType = HardDriveType.SSD,
                Capacity = 240,
                Price = 89.94m,
                Quantity = 424,
                Discount = 11,
                Name = "SanDisk Ultra II, 240GB, SATA III",
                Description = "Get accelerated performance from the brand trusted by pros. Featuring SanDisk's nCache 2.0 technology, the SanDisk Ultra II SSD delivers enhanced speed and endurance with sequential read speeds of up to 550MB/s and sequential write speeds of up to 500MB/s, for no-wait boot-up, shorter application load times, and quicker data transfer (1). In addition to cooler, quieter computing, the SanDisk Ultra II SSD includes proven shock and vibration resistance to protect your drive. From the company that invented the solid state technology that makes SSDs possible, the SanDisk Ultra II SSD catapults your performance to new levels.",
                ImageUrl = "https://www.sandisk.com/content/dam/sandisk-main/en_us/portal-assets/product-images/retail-products/Ultra_II_front-retina.png",
                Guarantee = GuaranteeDurationType.Thirty_Six_Months
            });
        }

        private void SeedGraphicCards(TechZoneContext context)
        {
            context.GraphicCards.AddOrUpdate(g => g.Name,
                new GraphicCard
                {
                    Brand = GraphicCardManufacturerType.Nvidia,
                    Manufacturer = ManufacturerType.Gigabyte,
                    MemoryType = GraphicCardMemoryType.GDDR5,
                    Description = "Powered by Nvidia GeForce GTX 770 GPU and integrated with industry's best 2GB GDDR5 memory and 384 bit memory interface",
                    Memory = 2,
                    Name = "Gigabyte GTX 770 GDDR5-2GB",
                    Price = 269,
                    Quantity = 24,
                    ImageUrl = "http://cdn5.thinkcomputers.org/wp-content/uploads/2013/06/gtx-770-windforce-2.jpg",
                    Guarantee = GuaranteeDurationType.Twelve_Months
                },
                new GraphicCard
                {
                    Brand = GraphicCardManufacturerType.Nvidia,
                    Manufacturer = ManufacturerType.eVGA,
                    MemoryType = GraphicCardMemoryType.GDDR5,
                    Description = "The EVGA GeForce GTX 1050 Ti hits the perfect spot for that upgrade you know you need, but at the price you want! With the latest NVIDIA Pascal architecture, the 4GB GTX 1050 Ti displays stunning visuals and great performance at 1080p HD+. Installing a EVGA GeForce GTX 1050 Ti gives you the power to take on today's next-gen titles in full 1080p HD - with room to spare. These cards give you a choice of memory sizes, cooling options, factory overclocks, and power options to fit every need and every system. Of course, no GTX card would be complete without essential gaming technologies, such as NVIDIA GameStream, G-Sync, and GeForce Experience. If you've been waiting for that card that gives you the performance to take back the competitive edge, but without taking out your wallet, then the GTX 1050 Ti is the card for you!",
                    Memory = 4,
                    Name = "EVGA GeForce GTX 1050 Ti, 4GB GDDR5",
                    Price = 139.99m,
                    Quantity = 12,
                    ImageUrl = "http://www.evga.com/products/images/gallery/04G-P4-6253-KR_XL_1.jpg",
                    Guarantee = GuaranteeDurationType.Thirty_Six_Months
                },
                new GraphicCard
                {
                    Brand = GraphicCardManufacturerType.Nvidia,
                    Manufacturer = ManufacturerType.eVGA,
                    MemoryType = GraphicCardMemoryType.GDDR5,
                    Description = "The EVGA GeForce GTX 1070 featuring EVGA ACX 3.0 cooling has arrived. This new graphics card features NVIDIA's new \"Pascal\" graphics processor which is the most advanced gaming GPU ever created. This breakthrough GPU delivers industry-leading performance, innovative new gaming technologies, and immersive, nextgen VR. These cards also feature EVGA ACX 3.0 cooling technology. EVGA ACX 3.0 once again brings new and exciting features to the award winning EVGA ACX cooling technology. SHP 2.0 gives increased heatpipes and copper contact area for cooler operation, and optimized fan curve for even quieter gaming. Of course, ACX 3.0 coolers also feature optimized swept fan blades, double ball bearings and an extreme low power motor, delivering more air flow with less power, unlocking additional power for the GPU.",
                    Memory = 8,
                    Name = "EVGA GeForce GTX 1070, 8GB GDDR5",
                    Price = 374,
                    Quantity = 132,
                    ImageUrl = "http://www.evga.com/products/images/gallery/08G-P4-6171-KR_XL_1.jpg",
                    Guarantee = GuaranteeDurationType.Sixty_Months
                },
                new GraphicCard
                {
                    Brand = GraphicCardManufacturerType.Nvidia,
                    Manufacturer = ManufacturerType.eVGA,
                    MemoryType = GraphicCardMemoryType.GDDR5,
                    Description = "The EVGA GeForce GTX 1080 Ti is NVIDIA's new flagship gaming GPU, based on the NVIDIA Pascal architecture. The latest addition to the ultimate gaming platform, this card is packed with extreme gaming horsepower, next-gen 11 Gbps GDDR5X memory, and a massive 11 GB frame buffer. The EVGA GeForce GTX 1080 Ti delivers truly next-generation VR performance, the lowest latency and plug-and-play compatibility with leading headsets. It's all driven by NVIDIA VRWorks technologies and brought to life with amazing VR audio, physics, and haptics. The next generation of EVGA Precision software has arrived with EVGA Precision XOC. This new version of Precision is built for the NVIDIA Pascal architecture and combines the best of EVGA Precision and EVGA OC Scanner to give you never before seen overclocking features and built in automatic overclocking tuning.",
                    Memory = 11,
                    Name = "EVGA GeForce GTX 1080 Ti, 11GB GDDR5X",
                    Price = 699.99m,
                    Discount = 15,
                    Quantity = 1,
                    ImageUrl = "http://www.evga.com/products/images/gallery/11G-P4-6390-KR_XL_1.jpg",
                    Guarantee = GuaranteeDurationType.Twenty_Four_Months
                },
                new GraphicCard
                {
                    Brand = GraphicCardManufacturerType.Nvidia,
                    Manufacturer = ManufacturerType.MSI,
                    MemoryType = GraphicCardMemoryType.GDDR5,
                    Description = "MSI NVIDIA GeForce GTX 1060 gaming x 6GB GDDR5 DVI/HDMI/3DisplayPort PCI-Express video card.",
                    Memory = 6,
                    Name = "MSI GeForce GTX 1060, 6GB GDDR5",
                    Price = 269.89m,
                    Quantity = 201,
                    ImageUrl = "https://d284x0ytlho6sy.cloudfront.net/images/400/AB84670_7.jpg",
                    Guarantee = GuaranteeDurationType.Sixty_Months
                },
                new GraphicCard
                {
                    Brand = GraphicCardManufacturerType.Nvidia,
                    Manufacturer = ManufacturerType.ASUS,
                    MemoryType = GraphicCardMemoryType.GDDR5,
                    Description = "Powered by NVIDIA Pascal, ROG Strix GTX 1080 brings performance and customization to another level with 1835 MHz Boost enabled by Super Alloy Power II and Aura RGB Lighting on the shroud & back plate. Direct CU III cools efficiently and quietly, while new ASUS Fan Connect provides dual 4-pin GPU-controlled PWM fan headers.",
                    Memory = 8,
                    Name = "ASUS GeForce GTX 1080 8GB ROG STRIX",
                    Price = 534.99m,
                    Quantity = 54,
                    ImageUrl = "https://www.asus.com/media/global/products/nrWaZwKol5KpK4Ud/P_setting_000_1_90_end_500.png",
                    Guarantee = GuaranteeDurationType.Twenty_Four_Months
                },
                new GraphicCard
                {
                    Brand = GraphicCardManufacturerType.Nvidia,
                    Manufacturer = ManufacturerType.MSI,
                    MemoryType = GraphicCardMemoryType.GDDR5,
                    Description = "The GeForce GTX 750 Ti is Nvidia's new Maxwell-based GPU that brings a new level of power efficiency. The MSI Gaming Series N750Ti TF 2GD5/OC packs such a GPU, and also offers gaming series features such as Twin Frozr IV cooler, military class components, and more. It is factory overclocked to 1085MHz Base Clock and 1163MHz Boost Clock for a delightful performance boost and higher framerates in actual gaming, but with only 60W of power consumption. This card also packs the latest GTX gaming technologies, such as NVIDIA Surround, 3D Vision, GPU BOOST 2.0, and PhysX.",
                    Memory = 2,
                    Name = "MSI NVIDIA GeForce GTX 750 Ti, 2GB GDDR5",
                    Price = 140.95m,
                    Quantity = 154,
                    ImageUrl = "http://i.ebayimg.com/images/i/151869595935-0-1/s-l1000.jpg",
                    Guarantee = GuaranteeDurationType.Twelve_Months
                },
                new GraphicCard
                {
                    Brand = GraphicCardManufacturerType.Nvidia,
                    Manufacturer = ManufacturerType.Palit,
                    MemoryType = GraphicCardMemoryType.GDDR5,
                    Description = "Turn your PC into a true gaming rig with the fast, powerful GeForce GTX 1050 Ti. It's powered by NVIDIA Pascal - the most advanced GPU architecture ever created and features innovative NVIDIA technologies to drive the latest games in their full glory. Box Contains: 1 x GeForce GTX 1050 Ti Graphics card Driver and Utility Disk Manual",
                    Memory = 4,
                    Name = "Palit GeForce GTX 1050 Ti KalmX, 4 GB GDDR5",
                    Price = 160.95m,
                    Quantity = 49,
                    Discount = 10,
                    ImageUrl = "https://www.quietpc.com/images/products/palit-ne5105t018g1-1070h-box-large.jpg",
                    Guarantee = GuaranteeDurationType.Twenty_Four_Months
                },
                new GraphicCard
                {
                    Brand = GraphicCardManufacturerType.Nvidia,
                    Manufacturer = ManufacturerType.MSI,
                    MemoryType = GraphicCardMemoryType.DDR3,
                    Description = "A great upgrade for your integrated graphics, the GeForce GT 730 accelerates the overall performance, including multimedia and gaming. Based on the industry-leading 28nm Kepler architecture, the GeForce GT 730 packs powerful NVIDIA SMX shaders and abundant amounts of memory that deliver a performance punch in all the latest 3D games and applications. An array of NVIDIA innovations, such as NVIDIA Surround, NVIDIA Adaptive Vertical Sync, and the new TXAA Anti-Aliasing Mode, offers more than one way to elevate your gaming experience. Bring your user experience in work and play to a new level!",
                    Memory = 2,
                    Name = "MSI nVIDIA GeForce GT 730 V2, 2048MB, DDR3",
                    Price = 59.99m,
                    Quantity = 13,
                    ImageUrl = "http://media.ldlc.com/ld/products/00/03/40/16/LD0003401692_2.jpg",
                    Guarantee = GuaranteeDurationType.Twelve_Months
                },
                new GraphicCard
                {
                    Brand = GraphicCardManufacturerType.Amd,
                    Manufacturer = ManufacturerType.ASUS,
                    MemoryType = GraphicCardMemoryType.DDR3,
                    Description = "AMD announced a new Product Brand Strategy in support of AMD Radeon R7 Series with latest technology and higher performance. ASUS R7240-2GD3-L is equipped with Super Alloy Power which reaches 15% performance increase, 2.5 times longer product lifespan and 35oC cooler operation. Dust-Proof Fan design additionally dissipates heat efficiently while expending lifespan by 25%. Moreover, ASUS R7240-2GD3-L comes with exclusive GPU Tweak utility, which allows users to modify clock speeds, voltages, and fan performance. R7240-2GD3-L with 2GB DDR3 Memory provides the best gaming experience & the best resolution. Also, the Low Profile Design is perfect for mini-ITX PCs.",
                    Memory = 2,
                    Name = "Asus AMD Radeon R7 240, 2048MB, DDR3",
                    Price = 61.51m,
                    Quantity = 132,
                    Discount = 20,
                    ImageUrl = "http://www.czone.com.pk/Images/Products/2355-18072014031211.jpg",
                    Guarantee = GuaranteeDurationType.Thirty_Six_Months
                },
                new GraphicCard
                {
                    Brand = GraphicCardManufacturerType.Amd,
                    Manufacturer = ManufacturerType.ASUS,
                    MemoryType = GraphicCardMemoryType.GDDR5,
                    Description = "The Radeon rebellion has begun, LED by the ROG STRIX rx 480 with DirectX III and aura RGB, bringing vr ready premium performance to the (stylish) masses. Clocking in at 1310 Mhz (OC mode) with 8GB VRAM and GPU tweak II, this card delivers 1-click overclocking to drive FPS higher.",
                    Memory = 8,
                    Name = "ASUS ROG STRIX Radeon Rx 480, 8GB",
                    Price = 239.99m,
                    Quantity = 15,
                    ImageUrl = "http://edgeup.asus.com/wp-content/uploads/2016/07/box-696x464.jpg",
                    Guarantee = GuaranteeDurationType.Twenty_Four_Months
                },
                new GraphicCard
                {
                    Brand = GraphicCardManufacturerType.Amd,
                    Manufacturer = ManufacturerType.MSI,
                    MemoryType = GraphicCardMemoryType.DDR3,
                    Description = "MSI ATI Radeon HD6450 1GB DDR3 VGA/DVI/HDMI Low Profile PCI-Express Video Card",
                    Memory = 1,
                    Name = "MSI ATI Radeon HD6450, 1 GB DDR3",
                    Price = 36.96m,
                    Quantity = 225,
                    ImageUrl = "https://images10.newegg.com/NeweggImage/ProductImage/14-127-584-10.jpg",
                    Guarantee = GuaranteeDurationType.Twelve_Months
                },
                new GraphicCard
                {
                    Brand = GraphicCardManufacturerType.Amd,
                    Manufacturer = ManufacturerType.Gigabyte,
                    MemoryType = GraphicCardMemoryType.GDDR5,
                    Description = "Powered by Radeon rx 470 and integrated with 4GB GDDR5 256bit memory",
                    Memory = 4,
                    Name = "Gigabyte Radeon Rx 470, 4GB GDDR5",
                    Price = 216.03m,
                    Quantity = 76,
                    Discount = 25,
                    ImageUrl = "http://static.gigabyte.com/Product/3/5990/20161117105741_big.png",
                    Guarantee = GuaranteeDurationType.Thirty_Six_Months
                },
                new GraphicCard
                {
                    Brand = GraphicCardManufacturerType.Amd,
                    Manufacturer = ManufacturerType.Gigabyte,
                    MemoryType = GraphicCardMemoryType.GDDR5,
                    Description = "Powered by Radeon rx 460 and integrated with 4GB GDDR5 128bit memory",
                    Memory = 4,
                    Name = "Gigabyte Radeon Rx 460, 4GB GDDR5",
                    Price = 121.34m,
                    Quantity = 23,
                    ImageUrl = "https://p1.akcdn.net/full/405542854.gigabyte-radeon-rx-460-windforce-oc-4gb-gddr5-128bit-pcie-gv-rx460wf2oc-4gd.jpg",
                    Guarantee = GuaranteeDurationType.Twelve_Months
                },
                new GraphicCard
                {
                    Brand = GraphicCardManufacturerType.Nvidia,
                    Manufacturer = ManufacturerType.MSI,
                    MemoryType = GraphicCardMemoryType.GDDR5,
                    Description = "DELETE MEDELETE MEDELETE MEDELETE MEDELETE MEDELETE MEDELETE MEDELETE MEDELETE MEDELETE MEDELETE MEDELETE MEDELETE ME",
                    Memory = 4,
                    Name = "Gigabyte Radeon DELLLLL",
                    Price = 121.34m,
                    Quantity = 23,
                    ImageUrl = "https://p1.akcdn.net/full/405542854.gigabyte-radeon-rx-460-windforce-oc-4gb-gddr5-128bit-pcie-gv-rx460wf2oc-4gd.jpg",
                    Guarantee = GuaranteeDurationType.Twenty_Four_Months
                });
        }

        private void SeedUsers(TechZoneContext context, UserManager<ApplicationUser> userManager)
        {
            var admin = userManager.FindByName("admin");
            if (admin == null)
            {
                var newUser = new ApplicationUser()
                {
                    UserName = "admin",
                    FirstName = "Admin",
                    LastName = "Adminer",
                    Email = "admin@g.c",
                };
                userManager.Create(newUser, "Pesho1!");
                userManager.SetLockoutEnabled(newUser.Id, false);
                userManager.AddToRole(newUser.Id, "Admin");
                context.Customers.Add(new Customer { UserId = newUser.Id, Credits = 10000 });
            }

            var pesho = userManager.FindByName("pesho");
            if (pesho == null)
            {
                var newUser = new ApplicationUser
                {
                    UserName = "pesho",
                    FirstName = "Petar",
                    LastName = "Dimitrov",
                    Email = "pesho@g.c",
                };
                userManager.Create(newUser, "Pesho1!");
                userManager.SetLockoutEnabled(newUser.Id, false);
                userManager.AddToRole(newUser.Id, "Admin");
                userManager.AddToRole(newUser.Id, "Customer");
                context.Customers.Add(new Customer { UserId = newUser.Id, Credits = 10000 });
            }

            var bojo = userManager.FindByName("bojo");
            if (bojo == null)
            {
                var newUser = new ApplicationUser
                {
                    UserName = "bojo",
                    FirstName = "Bozhidar",
                    LastName = "Gevechanov",
                    Email = "bojo@g.c",
                };
                userManager.Create(newUser, "Pesho1!");
                userManager.SetLockoutEnabled(newUser.Id, false);
                userManager.AddToRole(newUser.Id, "Publisher");
                context.Customers.Add(new Customer { UserId = newUser.Id });
            }

            var joro = userManager.FindByName("joro");
            if (joro == null)
            {
                var newUser = new ApplicationUser()
                {
                    UserName = "joro",
                    FirstName = "Georgi",
                    LastName = "Stoimenov",
                    Email = "joro@g.c",
                };
                userManager.Create(newUser, "Pesho1!");
                userManager.SetLockoutEnabled(newUser.Id, false);
                userManager.AddToRole(newUser.Id, "Publisher");
                context.Customers.Add(new Customer { UserId = newUser.Id });
            }

            var jicata = userManager.FindByName("jicata");
            if (jicata == null)
            {
                var newUser = new ApplicationUser
                {
                    UserName = "jicata",
                    FirstName = "Svetlin",
                    LastName = "Galov",
                    Email = "jicata@g.c",
                };
                userManager.Create(newUser, "Pesho1!");
                userManager.SetLockoutEnabled(newUser.Id, false);
                userManager.AddToRole(newUser.Id, "Customer");
                context.Customers.Add(new Customer { UserId = newUser.Id, Credits = 10000 });
            }

            context.SaveChanges();
        }

        private void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExists("Admin"))
                roleManager.Create(new IdentityRole("Admin"));
            if (!roleManager.RoleExists("Publisher"))
                roleManager.Create(new IdentityRole("Publisher"));
            if (!roleManager.RoleExists("Moderator"))
                roleManager.Create(new IdentityRole("Moderator"));
            if (!roleManager.RoleExists("Customer"))
                roleManager.Create(new IdentityRole("Customer"));
        }
    }
}