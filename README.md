


# Welcome to SRSInventory!
I'm sorry, but I will continue in hungarian... :)
---
Ez a repó azért készült, hogy valamilyen szinten bemutathassam szoftverfejlesztői képességeimet - illetve mert kérték :)

# Technikai döntések
Igyekeztem az **OOP**, **SOLID** és nem utolsó sorban az **MVVM** mintákat követni, illetve felhasználni a megfelelő nuget csomagokat azért, hogy a feladat megoldása minnél gyorsabb és jobb legyen.

MAUI fejlesztésben munkahelyi szinten nem sok tapasztalatom van, de amit sikerült elsajátítanom hobbi szinten, azokat a saját projectekben sikeresen tudtam hasznosítani.

## Megoldások, részfeladatok

A loggolást nyílván NLog-ra bíznám, így a kódban is csak egy dummy loggert használtam, mivel a loggolás viszont nagyon fontos a support miatt.

Az adatbázis-kezelést lehetne EntityFramework-re bízni (pl CodeFirst), de mivel ez egy *test* project, ezért egy szimpla DatabaseService-t implementáltam.

"Sajnos" 2026-ban már az AI-é a főszerep, én nem használtam fel kódok generálására, review-ra, csak ha elakadtam néha feltettem néhány kérdést, amire nem mindig kaptam kiválló minőségű választ, de azért a segítségemre volt, ezt nem tagadom. ;)

## További fejlesztési lehetőségek
A sok elemű collection kezelését egy saját (vagy már meglévő nuget csomag készleteként) ObservableRangeCollection implementációval oldanám meg, ahol az OnCollectionChanged event nem hívódik meg minden egyes Add-ra (pl. AddRange).

Korábbi munkahelyeimen nagy hangsújt fektettem a UX-be, itt is megpróbáltam egy mai, elfogadható stílust kialakítani, de azért lenne mit rajta javítani (pl ikonok, képek, apró betűs leírások, stb...).

Unit tesztek nem készültek, mert nem volt a feladat része, de természetesen az is fontos része a szoftverfejlesztésnek, így azt is nagyon fontosnak tartom.

## Összegzés
A feladott feladat érdekes volt, megfelelően nehéz, de megoldható.
Minden igyekezetemmel azon voltam, hogy elfogadható eredményt nyújtsak, remélem sikerült.

Izgatottam várom pozitív / negatív visszajelzésüket,
tisztelettel,\
Dankovics József