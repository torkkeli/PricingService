Jag har inte testat att anropa APIt eftersom jag hade problem med att registrera MediatR dependencies
(Koden �r utkommenterad i Application.Configuration)

Lite antaganden om data modellen:
 * En Customer m�ste ha en CustomerService instans och bara en per PriceServiceType (A, B, C) Som �r aktiv under den ber�knande perioden
 * En CustomerService m�ste ha ett StartDate
 * En CustomerService kan ha flera discounts under olika perioder men inte flera under samma period