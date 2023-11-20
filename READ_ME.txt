Jag har inte testat att anropa APIt eftersom jag hade problem med att registrera MediatR dependencies
(Koden är utkommenterad i Application.Configuration)

Lite antaganden om data modellen:
 * En Customer måste ha en CustomerService instans och bara en per PriceServiceType (A, B, C) Som är aktiv under den beräknande perioden
 * En CustomerService måste ha ett StartDate
 * En CustomerService kan ha flera discounts under olika perioder men inte flera under samma period