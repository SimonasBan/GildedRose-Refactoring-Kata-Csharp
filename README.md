# GildedRose-Refactoring-Kata-Csharp
Gilded Rose Refactoring Kata solution for 'Telesoftas' internship.

## Changes made to the original code:
•The original code has been refactored on a switch statement since
the task asked to make the code readable
and maintanable. This would make it easier to add new
items in the future.

•Every item has it's own Handling method, which also adds to the readability and maintainability.

•Recurring values put into constant values.
## New features:
• Exception handling has been added for the items with Quality value that does not pass the requirements.

• 'Conjured' item handling.

## Other ideas I've had
• If I were able change Item class, I would have added itemType property to it and made switch based on itemType.
But based on the task I couldn't. I thought about doing Polymorphism based on the Item class to find a way around 
but I suppose that the requirement:
'do not alter the Item class or Items property'
does not allow
 me to do that as well since I wouldn't use
Items property anymore.

