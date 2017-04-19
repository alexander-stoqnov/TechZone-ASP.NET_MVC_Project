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

            //SeedRoles(roleManager);
            //SeedUsers(context, userManager);
            //SeedGraphicCards(context);
            //SeedHardDrives(context);
            context.SaveChanges();
        }

        private void SeedHardDrives(TechZoneContext context)
        {
            context.HardDrives.AddOrUpdate(h => h.Name, new HardDrive
            {
                DriveBrand = HardDriveBrandType.Seagate,
                DriveType = HardDriveType.HDD,
                Capacity = 1000,
                Price = 53.99m,
                Quantity = 550,
                Name = "Seagate Expansion, 1TB Portable External Hard Drive, USB 3.0",
                Description = "The Seagate Expansion portable drive provides extra storage for your ever-growing collection of files. Instantly add space for more files, consolidate all of your files to a single location, or free up space on your computer's internal drive to help improve performance. Setup is straightforward; simply plug in the included USB cable, and you are ready to go. The drive is automatically recognized by the Windows operating system, so there is no software to install or configure. Saving files is easy too–just drag-and-drop. Take advantage of the fast data transfer speeds with the USB 3.0 interface by connecting to a SuperSpeed USB 3.0 port. USB 3.0 is backwards compatible with USB 2.0 for additional system compatibility. System Requirements: Windows 7 or higher, SuperSpeed USB 3.0 port (required for USB 3.0 transfer speeds or backwards compatible with USB 2.0 ports at USB 2.0 transfer speeds, compatibility may vary depending on the user's hardware configuration and operating system). Box Contents: Seagate Expansion Drive, 18-inch USB 3.0 Cable, and Quick Start Guide.",
                ImageUrl = "http://s.productreview.com.au/products/images/d850c9c0-04a8-4d94-8fd5-14e8a2ae28b1.jpeg"
            }, new HardDrive
            {
                DriveBrand = HardDriveBrandType.WesternDigital,
                DriveType = HardDriveType.HDD,
                Capacity = 2000,
                Price = 76.40m,
                Quantity = 350,
                Name = "WD 2TB Elements Portable External Hard Drive - USB 3.0",
                Description = "WD Elements portable hard drives with USB 3.0 offer reliable, high-capacity storage to go, ultra-fast data transfer rates and universal connectivity with USB 2.0 and USB 3.0 devices. The small, lightweight enclosure features massive capacity and WD quality and reliability. It includes a free trial of WD SmartWare Pro backup software for local and cloud backup.",
                ImageUrl = "https://images10.newegg.com/ProductImage/22-236-519-05.jpg"
            }, new HardDrive
            {
                DriveBrand = HardDriveBrandType.Seagate,
                DriveType = HardDriveType.HDD,
                Capacity = 5000,
                Price = 119.99m,
                Quantity = 120,
                Name = "Seagate Expansion 5TB Desktop External Hard Drive USB 3.0",
                Description = "The Seagate Expansion desktop drive provides extra storage for your ever-growing collection of files. Instantly add space for more files, consolidate all of your files to a single location, or free up space on your computer's internal drive to help improve performance. Setup is straightforward; simply plug in the included power supply and USB cable, and you are ready to go. It is automatically recognized by the Windows operating system, so there is no software to install and nothing to configure. Saving files is easy too-just drag-and-drop. Take advantage of the fast data transfer speeds with the USB 3.0 interface by connecting to a SuperSpeed USB 3.0 port. USB 3.0 is backwards compatible with USB 2.0 for additional system compatibility. System Requirements: Windows 7 or higher, SuperSpeed USB 3.0 port (required for USB 3.0 transfer speeds or backwards compatible with USB 2.0 ports at USB 2.0 transfer speeds, compatibility may vary depending on the user's hardware configuration and operating system). Box Contents: Seagate Expansion Drive, 4-foot (120cm) USB 3.0 Cable, Power adapter and Quick Start Guide.",
                ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/617Fg8dJCsL._SL1267_.jpg"
            }, new HardDrive
            {
                DriveBrand = HardDriveBrandType.WesternDigital,
                DriveType = HardDriveType.HDD,
                Capacity = 1000,
                Price = 49.99m,
                Quantity = 430,
                Name = "WD Purple 1TB Surveillance Hard Drive, 5400 RPM, 6 Gb/s",
                Description = "WD Purple surveillance storage is built to handle up to 32 HD cameras per drive and is designed for 24/7, always on, high-definition surveillance security systems that use up to eight hard drives.",
                ImageUrl = "http://www.ctiai.com/store/media/catalog/product/cache/2/image/800x800/9df78eab33525d08d6e5fb8d27136e95/1/t/1tb.png"
            }, new HardDrive
            {
                DriveBrand = HardDriveBrandType.Toshiba,
                DriveType = HardDriveType.HDD,
                Capacity = 500,
                Price = 36.50m,
                Quantity = 250,
                Name = "Toshiba 3.5-Inch, 500GB, 7200 RPM SATA3, 6.0 GB/s",
                Description = "Toshiba DT01ACA050 500GB 7200RPM SATA3/SATA 6.0 GB/s 32MB Hard Drive (3.5 inch)",
                ImageUrl = "http://compareindia.news18.com/media/gallery/images/2012/feb/mk5065gsx_2_231114491599.jpg"
            }, new HardDrive
            {
                DriveBrand = HardDriveBrandType.Seagate,
                DriveType = HardDriveType.HDD,
                Capacity = 2000,
                Price = 66.99m,
                Quantity = 99,
                Name = "Seagate 2TB BarraCuda SATA 6Gb/s",
                Description = "Versatile and dependable, the fierce Seagate Barracuda drives build upon a reliable drive family spanning 20 years. Count on affordable Barracuda drives as 2.5 and 3.5 inch HDD solutions for nearly any application-working, playing and storing your movies and music.",
                ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/51oSffVPIWL._SY355_.jpg"
            }, new HardDrive
            {
                DriveBrand = HardDriveBrandType.WesternDigital,
                DriveType = HardDriveType.HDD,
                Capacity = 500,
                Price = 58.45m,
                Quantity = 33,
                Name = "WD Black 500GB, 7200 RPM, 6 Gb/s",
                Description = "WD Black hard drives are designed for enthusiasts and creative professionals looking for leading-edge performance. These 2.5-inch mobile drives are perfect for high-performance applications like photo and video editing, gaming and power PCs. Supported by industry leading 5 year limited warranty.",
                ImageUrl = "http://www.stuartconnections.com/9013-thickbox/Western-Digital-Scorpio-Black-500GB-7200RPM-SATA-II-Laptop-Notebook-Hard-Drive-WD5000BPKT.jpg"
            }, new HardDrive
            {
                DriveBrand = HardDriveBrandType.Samsung,
                DriveType = HardDriveType.SSD,
                Capacity = 250,
                Price = 109.99m,
                Quantity = 266,
                Name = "Samsung 850 EVO 250GB SSD",
                Description = "Samsung's 850 EVO series SSD is the industry's #1 best-selling* SSD and is perfect for everyday computing. Powered by Samsung's V-NAND technology, the 850 EVO transforms the everyday computing experience with optimized performance and endurance. Designed to fit desktop PCs, laptops, and ultrabooks, the 850 EVO comes in a wide range of capacities and form factors. *Based on 2015 NPD reported revenue in the US.",
                ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/71gkS5vep8L._SL1500_.jpg"
            }, new HardDrive
            {
                DriveBrand = HardDriveBrandType.Samsung,
                DriveType = HardDriveType.SSD,
                Capacity = 500,
                Price = 177.92m,
                Quantity = 121,
                Name = "Samsung 850 EVO 500GB SSD",
                Description = "Upgrading your PC with a Samsung SSD is the most economical way to breathe new life into an aging PC. The 850 EVO reads, writes and multi-tasks at incredible speeds, enhancing boot-up speed, application loading and multi-tasking performance. It's more than an upgrade, it's a complete transformation of your PC.",
                ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/91ZPflI8tzL._SX466_.jpg"
            }, new HardDrive
            {
                DriveBrand = HardDriveBrandType.Kingston,
                DriveType = HardDriveType.SSD,
                Capacity = 120,
                Price = 52,
                Quantity = 54,
                Name = "Kingston Digital, 120GB, SSDNow UV400",
                Description = "Kingston's SSDnow UV400 is powered by a four-channel Marvell controller for incredible speeds and higher performance compared to a mechanical hard drive. It dramatically improves the responsiveness of your existing system and is 10 times faster than a 7200RPM hard drive (based on “out-of-box performance” using a SATA Rev. 3.0 motherboard. Speed may vary due to host hardware, software, and usage). Rugged and more reliable and durable than a hard drive, UV400 is built using flash memory so it's shock- and vibration-resistant and less likely to fail than a mechanical hard drive. Its ruggedness makes it ideal for notebooks and other mobile computing devices. UV400 is available in multiple capacities, giving you plenty of space for all your files, applications, videos, photos and other important documents. It's the ideal hard drive Replacement and can also replace a smaller SSD in your system to give you all the room you need.",
                ImageUrl = "https://images10.newegg.com/ProductImage/20-242-257-01.jpg"
            }, new HardDrive
            {
                DriveBrand = HardDriveBrandType.Kingston,
                DriveType = HardDriveType.SSD,
                Capacity = 240,
                Price = 109.99m,
                Quantity = 103,
                Name = "Kingston Digital HyperX FURY 240GB ",
                Description = "HyperX® FURY solid-state drive delivers impressive performance at an affordable price, to get you into the game faster and improve your gameplay. Ideal for desktops and notebooks, it features a slim 7mm form factor and SandForce® SF-2281 controller with SATA Rev 3.0 (6Gb/s) performance. The result is faster system boot up, application loading and file execution plus faster map and level load time. Its synchronous NAND allows for higher and consistent drive performance. So you're not only in the game, you're winning it. In 120GB and 240GB capacities, HyperX FURY SSD is available as a stand-alone drive and is easy to install. This cost-efficient upgrade is less disruptive than buying a new system to increase performance. The new FURY entry-level product line from HyperX includes memory and SSDs and is designed for gamers, enthusiasts and system Integrators, who can now have consistent HyperX branding for their gaming PCs. Built with Flash memory, HyperX FURY SSD has no moving parts and is cooler, quieter and more shock- and vibration-resistant than traditional hard drives, making it the ideal hard drive replacement. It's backed by a three-year warranty and free technical support. / Kingston Hyperx FURY 240 GB 2.5\" Internal Solid State Drive - SATA - 500 MB / s Maximum Read Transfer Rate - 500 MB / s Maximum Write Transfer Rate - 22500IOPS Random 4KB Read - 41000IOPS Random 4KB Write / SATA - 500 MB / s Maximum Read Transfer Rate - 500 MB / s Maximum Write Transfer Rate - 22500IOPS Random 4KB Read - 41000IOPS Random 4KB Write.",
                ImageUrl = "http://images.hardwarecanucks.com/image/akg/Storage/Fury/top_sm.jpg"
            }, new HardDrive
            {
                DriveBrand = HardDriveBrandType.SanDisk,
                DriveType = HardDriveType.SSD,
                Capacity = 240,
                Price = 93.99m,
                Quantity = 11,
                Name = "Sandisk Z400s 256GB",
                Description = "The Sandisk z400s SSD delivers the performance, capacities, and form factors ideal for replacing HDDs in embedded and mainstream computing platforms. It can outperform HDD by a factor of 20, while providing 5x the reliability at 1/20th the power consumption.",
                ImageUrl = "https://images10.newegg.com/NeweggImage/ProductImage/A85V_1_20160118617953317.jpg"
            }, new HardDrive
            {
                DriveBrand = HardDriveBrandType.WesternDigital,
                DriveType = HardDriveType.SSD,
                Capacity = 240,
                Price = 84.99m,
                Quantity = 44,
                Name = "WD Blue 250GB, SSD, SATA 6Gb/s",
                Description = "With superior performance and a leader in reliability, the WD Blue SSD offers digital storage that is optimized for multi-tasking and ready to keep up with your high performance computing needs. Available in both 2.5\" / 7mm cased and M.2 2280 form factors and WD's Functional Integrity Testing Lab (FIT Lab), the WD Blue SSD is compatible with a wide range of computers, so you can be sure you are making the right choice. Combined with the free, downloadable WD SSD Dashboard and a 3-year limited warranty, you can confidently upgrade your system to the WD Blue SSD.",
                ImageUrl = "http://i.expansys.com/i/b/b296045-1.jpg"
            }, new HardDrive
            {
                DriveBrand = HardDriveBrandType.SanDisk,
                DriveType = HardDriveType.SSD,
                Capacity = 240,
                Price = 89.94m,
                Quantity = 424,
                Name = "SanDisk Ultra II, 240GB, SATA III",
                Description = "Get accelerated performance from the brand trusted by pros. Featuring SanDisk's nCache 2.0 technology, the SanDisk Ultra II SSD delivers enhanced speed and endurance with sequential read speeds of up to 550MB/s and sequential write speeds of up to 500MB/s, for no-wait boot-up, shorter application load times, and quicker data transfer (1). In addition to cooler, quieter computing, the SanDisk Ultra II SSD includes proven shock and vibration resistance to protect your drive. From the company that invented the solid state technology that makes SSDs possible, the SanDisk Ultra II SSD catapults your performance to new levels.",
                ImageUrl = "https://www.sandisk.com/content/dam/sandisk-main/en_us/portal-assets/product-images/retail-products/Ultra_II_front-retina.png"
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
                    ImageUrl = "http://cdn5.thinkcomputers.org/wp-content/uploads/2013/06/gtx-770-windforce-2.jpg"
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
                    ImageUrl = "http://www.evga.com/products/images/gallery/04G-P4-6253-KR_XL_1.jpg"
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
                    ImageUrl = "http://www.evga.com/products/images/gallery/08G-P4-6171-KR_XL_1.jpg"
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
                    Quantity = 1,
                    ImageUrl = "http://www.evga.com/products/images/gallery/11G-P4-6390-KR_XL_1.jpg"
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
                    ImageUrl = "https://d284x0ytlho6sy.cloudfront.net/images/400/AB84670_7.jpg"
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
                    ImageUrl = "https://www.asus.com/media/global/products/nrWaZwKol5KpK4Ud/P_setting_000_1_90_end_500.png"
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
                    ImageUrl = "http://i.ebayimg.com/images/i/151869595935-0-1/s-l1000.jpg"
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
                    ImageUrl = "https://www.quietpc.com/images/products/palit-ne5105t018g1-1070h-box-large.jpg"
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
                    ImageUrl = "http://media.ldlc.com/ld/products/00/03/40/16/LD0003401692_2.jpg"
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
                    ImageUrl = "http://www.czone.com.pk/Images/Products/2355-18072014031211.jpg"
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
                    ImageUrl = "http://edgeup.asus.com/wp-content/uploads/2016/07/box-696x464.jpg"
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
                    ImageUrl = "https://images10.newegg.com/NeweggImage/ProductImage/14-127-584-10.jpg"
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
                    ImageUrl = "http://static.gigabyte.com/Product/3/5990/20161117105741_big.png"
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
                    ImageUrl = "https://p1.akcdn.net/full/405542854.gigabyte-radeon-rx-460-windforce-oc-4gb-gddr5-128bit-pcie-gv-rx460wf2oc-4gd.jpg"
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
                    ImageUrl = "https://p1.akcdn.net/full/405542854.gigabyte-radeon-rx-460-windforce-oc-4gb-gddr5-128bit-pcie-gv-rx460wf2oc-4gd.jpg"
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
                context.Customers.Add(new Customer() { UserId = newUser.Id });
            }

            var pesho = userManager.FindByName("pesho");
            if (pesho == null)
            {
                var newUser = new ApplicationUser()
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
                context.Customers.Add(new Customer() { UserId = newUser.Id });
            }

            var bojo = userManager.FindByName("bojo");
            if (bojo == null)
            {
                var newUser = new ApplicationUser()
                {
                    UserName = "bojo",
                    FirstName = "Bozhidar",
                    LastName = "Gevechanov",
                    Email = "bojo@g.c",
                };
                userManager.Create(newUser, "Pesho1!");
                userManager.SetLockoutEnabled(newUser.Id, false);
                userManager.AddToRole(newUser.Id, "Publisher");
                context.Publishers.Add(new Publisher() { UserId = newUser.Id });
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
                context.Publishers.Add(new Publisher() { UserId = newUser.Id });
            }

            var jicata = userManager.FindByName("jicata");
            if (jicata == null)
            {
                var newUser = new ApplicationUser()
                {
                    UserName = "jicata",
                    FirstName = "Svetlin",
                    LastName = "Galov",
                    Email = "jicata@g.c",
                };
                userManager.Create(newUser, "Pesho1!");
                userManager.SetLockoutEnabled(newUser.Id, false);
                userManager.AddToRole(newUser.Id, "Customer");
                context.Customers.Add(new Customer() { UserId = newUser.Id });
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