# README - Projekt Gry Wspó³pracy
## 1. Opis Projektu
### Od strony biznesowej
Nasz projekt to gra wspó³pracy dla dwóch graczy, ³¹cz¹ca elementy eksploracji, rozwi¹zywania zagadek i komunikacji.
**Cel**: Stworzenie immersyjnego œrodowiska, gdzie gracze musz¹ wspó³pracowaæ, by osi¹gn¹æ sukces. Projekt mo¿e znaleŸæ zastosowanie jako narzêdzie edukacyjne rozwijaj¹ce umiejêtnoœci pracy zespo³owej lub jako innowacyjna gra rozrywkowa.
**Rozwi¹zania**:
- Mechanika dwóch po³¹czonych œwiatów: gry i strony internetowej.
- Budowanie zaanga¿owania poprzez kooperacyjne zagadki.
- Oferowanie nietypowego modelu rozgrywki ³¹cz¹cego technologie gier i aplikacji webowych.

### Od strony technicznej
**U¿yte technologie**:
- **Frontend**: HTML, CSS, JavaScript (interfejs strony internetowej jako "konsola" dla drugiego gracza).
- **Backend**: Unity 3D (silnik gry), po³¹czony za pomoc¹ WebSocketów z przegl¹dark¹.
- **API**: W³asnorêcznie stworzone API do komunikacji miêdzy gr¹ a stron¹ internetow¹.
- **Baza danych**: Brak potrzeby przechowywania d³ugotrwa³ych danych, projekt opiera siê na wymianie informacji w czasie rzeczywistym.

## 2. Funkcjonalnoœci
- **Mechanika drzwi**: Gracze musz¹ wspó³pracowaæ, aby otwieraæ przejœcia w grze.
- **Podnoszenie przedmiotów**: Mechanika umo¿liwiaj¹ca zbieranie wa¿nych dla rozgrywki obiektów oraz przesuwanie ich w celu rozwi¹zania zagadek.

## 3. Problemy i ich rozwi¹zania
- **Synchronizacja miêdzy gr¹ a stron¹**: Trudnoœci z prawid³owym przekazywaniem danych w czasie rzeczywistym rozwi¹zano poprzez wdro¿enie optymalnych metod WebSocketowych.
- **Kamera gracza**: Niestabilnoœæ obrotów kamery podczas intensywnego ruchu zosta³a naprawiona przy u¿yciu `Mathf.Clamp` do ograniczenia zakresu patrzenia oraz precyzyjniejszej obs³ugi rotacji osi X i Y.
- **Mechanika skakania**: Kumulacja si³y przy wielokrotnych podskokach rozwi¹zana przez resetowanie osi Y prêdkoœci po wykonaniu skoku.

## 4. Komunikacja
Gra Unity komunikuje siê z serwerem WebSocket, przesy³aj¹c zdarzenia za pomoc¹ eventów, takich jak na przyk³ad próba utworzenia sesji (`GS_tryCreateSession`). Serwer odbiera te zdarzenia, przetwarza je i podejmuje odpowiednie akcje. W przypadku zdarzenia od Unity serwer mo¿e utworzyæ now¹ sesjê i zwróciæ do klienta (gry) wygenerowany token sesji.
Strona WWW korzysta z WebSocketów do przesy³ania zdarzeñ, takich jak logowanie do istniej¹cej sesji (`WS_tryLoginSession`) czy modyfikowanie stanu elementów (`WS_tryToggleElementState`, `WS_tryRangeElementState`). Serwer weryfikuje token sesji, aby upewniæ siê, ¿e ¿¹danie pochodzi od uprawnionego klienta. Jeœli weryfikacja siê powiedzie, serwer przesy³a odpowiedni¹ wiadomoœæ zwrotn¹ do Unity lub strony WWW, informuj¹c o powodzeniu lub niepowodzeniu operacji.
Ca³oœæ komunikacji dzia³a w oparciu o zdarzenia WebSocketowe, które pozwalaj¹ na bie¿¹c¹ wymianê danych miêdzy gr¹, serwerem i stron¹ w czasie rzeczywistym. Serwer pe³ni rolê centralnego punktu koordynuj¹cego wszystkie interakcje.

## 5. Linki
