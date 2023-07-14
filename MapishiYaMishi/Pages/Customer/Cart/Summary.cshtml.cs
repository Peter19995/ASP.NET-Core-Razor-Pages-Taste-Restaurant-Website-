using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Mishi.DataAccess.Repository.IRepository;
using Mishi.Models;
using Mishi.Utility;
using Stripe.Checkout;
using System.Security.Claims;

namespace MishiWeb.Pages.Customer.Cart
{
	[Authorize]
	[BindProperties]
	public class SummaryModel : PageModel
	{
		public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }

		public OrderHeader OrderHeader { get; set; }
		private readonly IUnitOfWork _unitOfWork;

		public SummaryModel(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			OrderHeader = new OrderHeader();
		}

		public void OnGet()
		{
			var claimIdentity = (ClaimsIdentity)User.Identity;
			var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);

			if (claim != null)
			{
				ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(filter: u => u.ApplicationUserId == claim.Value, includeProperties: "MenuItem,MenuItem.FoodType,MenuItem.Category");
				foreach (var cart in ShoppingCartList)
				{
					OrderHeader.OrderTotal += (cart.MenuItem.Price * cart.Count);
				}

				ApplicationUser applicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == claim.Value);
				OrderHeader.PickUpName = applicationUser.FirtsName + " " + applicationUser.LastName;
				OrderHeader.PickUpPhoneNumber = applicationUser.PhoneNumber;
			}
		}
		public IActionResult OnPost()
		{
			var claimIdentity = (ClaimsIdentity)User.Identity;
			var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);

			if (claim != null)
			{
				ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(filter: u => u.ApplicationUserId == claim.Value, includeProperties: "MenuItem,MenuItem.FoodType,MenuItem.Category");
				foreach (var cart in ShoppingCartList)
				{
					OrderHeader.OrderTotal += (cart.MenuItem.Price * cart.Count);
				}

				OrderHeader.Status = SD.StatusPending;
				OrderHeader.OrderDate = DateTime.Now;
				OrderHeader.UserId = claim.Value;
				OrderHeader.PickUpTime = Convert.ToDateTime(OrderHeader.PickUpDate.ToShortDateString() + " " + OrderHeader.PickUpTime.ToShortTimeString());
				_unitOfWork.OrderHeader.Add(OrderHeader);
				_unitOfWork.Save();

				foreach (var item in ShoppingCartList)
				{
					OrderDetails orderDetails = new()
					{
						MenuItedId = item.MenuItemId,
						OrderId = OrderHeader.Id,
						Name = item.MenuItem.Name,
						Price = item.MenuItem.Price,
						Count = item.Count
					};
					_unitOfWork.OrderDetails.Add(orderDetails);
				}
				
				//_unitOfWork.ShoppingCart.RemoveRange(ShoppingCartList);
				_unitOfWork.Save();

				var domain = "https://localhost:44344/";
				var options = new Stripe.Checkout.SessionCreateOptions
				{
                    LineItems = new List<SessionLineItemOptions>(),
                    PaymentMethodTypes = new List<string>
                    {
                        "card",
                    },
                    Mode = "payment",
                    SuccessUrl = domain + $"customer/cart/OrderConfirmation?id={OrderHeader.Id}",
                    CancelUrl = domain + $"Customer/Cart/index",

                };


				//add line Items
				foreach(var item in ShoppingCartList) {
					var SessionLineItem = new SessionLineItemOptions
					{
						PriceData = new SessionLineItemPriceDataOptions
						{
							UnitAmount = (long)(item.MenuItem.Price * 100),
							Currency = "usd",

							ProductData = new SessionLineItemPriceDataProductDataOptions
							{
								Name = item.MenuItem.Name,

							},

						},
						Quantity = item.Count
					};

					options.LineItems.Add(SessionLineItem);

                }

                var service = new Stripe.Checkout.SessionService();
				Session session = service.Create(options);
				Response.Headers.Add("Location", session.Url);

				OrderHeader.SessionId = session.Id;
                OrderHeader.PaymentIntentId = session.PaymentIntentId;
				_unitOfWork.Save();
                return new StatusCodeResult(303);

			}

			return Page();
		}

	}
}
