# TechZone-ASP.NET_MVC_Project
Final Project defense for SoftUni ASP.NET course

Official website : http://techzone-softuni-asp.azurewebsites.net/

Guest users :
1)	Can visit 90% of all pages anonymously. 
2)	Can add to shopping cart even without account.
3)	Can read articles, reviews, comments
4)	Their shopping cart items are kept after they register
Customers :
1)	Can continue to checkout to see the final cost of their purchase
2)	Shipping costs depend on distance to central office in Pleven. Also all kind of checks are made whether the user has enough credits.
3)	Can write ONE review per product. Product rating automatically updated
4)	Can upvote/downvote once per review
5)	Can post infinite amount of comments
6)	Can report an offensive/inappropriate comment
7)	If a user received more than 3 warnings he can request a chat meeting with a moderator
8)	Can review their purchase history, which holds pdf files of all orders, and the price of the product at the time of purchase (DROPBOX)
9)	Can upload their own profile picture

Admins :
1)	Can add new products.
2)	Can edit all user roles
3)	Can control all the current products that are in the store. Enable/Disable or edit information

Moderator :
1)	Can read inappropriate comments reports
2)	Can decide whether to punish or dismiss a report
3)	If a user requests a chat meeting, they can see a link to the room, and enter to chat (SignalR)

Products: Can be filtered using ODATA with all kinds of parameters. Loaded with AJAX

Articles: Can click on author name to show all the publications by the selected author.  Can search all articles with AJAX request.
