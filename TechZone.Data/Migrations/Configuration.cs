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

            // SeedRoles(roleManager);
            // SeedUsers(context, userManager);
            SeedGraphicCards(context);
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