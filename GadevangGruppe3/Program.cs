﻿using GadevangGruppe3Razor.Models;
using GadevangGruppe3Razor.Services;
using System.Security.AccessControl;
Console.WriteLine("Hello World");

#region Johanne
// Test Create Bane - virker 30/4 
Bane b1 = new Bane(1, BaneType.Tennis, BaneMiljø.Udendørs, "Grusbane");
Console.WriteLine(b1.ToString());


/* // Test GetAllBaneAsync - virker 4/10 
Console.Clear();
BaneService bs = new BaneService();
List<Bane> baner = await bs.GetAllBaneAsync();
foreach (Bane b in baner)
{
    Console.WriteLine(b.ToString());
}
Console.ReadKey();
 */

// Test Create og delete booking -  
BookingService bs = new BookingService();
Booking bb = new Booking(1, 1, new DateOnly(2025, 06, 23), 12, 1,1, "");
Console.WriteLine(bb.ToString());
bs.DeleteBookingAsync(bb.BookingId);
if (bb == null) 
{
    Console.WriteLine("Booking er slettet");
}
else 
{ Console.WriteLine(bb.ToString()); }



#endregion

#region Magnus
/* // Test Create Bruger
Bruger b1 = new Bruger("Vibeke Sandau", "JKL82", "VISA@gtmail.dk", "33333333", MedlemskabsType.Seniorer, Position.Medlem, true);
Console.WriteLine(b1);
*/

#endregion

#region Vincent
/* // Test Create Begivenhed - Virker 01/05
Begivenhed b1 = new Begivenhed(999, "General Forsamling", "Klubhuset", new DateTime(2025, 02, 16, 18, 30, 00), "Vi mødes for at diskutere budgettet og begivenheder til den kommende sæson", 40, 0);
Console.WriteLine(b1.ToString());
*/
#endregion




