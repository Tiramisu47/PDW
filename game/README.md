# README - Projekt Gry Wsp�pracy
## 1. Opis Projektu
### Od strony biznesowej
Nasz projekt to gra wsp�pracy dla dw�ch graczy, ��cz�ca elementy eksploracji, rozwi�zywania zagadek i komunikacji.
**Cel**: Stworzenie immersyjnego �rodowiska, gdzie gracze musz� wsp�pracowa�, by osi�gn�� sukces. Projekt mo�e znale�� zastosowanie jako narz�dzie edukacyjne rozwijaj�ce umiej�tno�ci pracy zespo�owej lub jako innowacyjna gra rozrywkowa.
**Rozwi�zania**:
- Mechanika dw�ch po��czonych �wiat�w: gry i strony internetowej.
- Budowanie zaanga�owania poprzez kooperacyjne zagadki.
- Oferowanie nietypowego modelu rozgrywki ��cz�cego technologie gier i aplikacji webowych.

### Od strony technicznej
**U�yte technologie**:
- **Frontend**: HTML, CSS, JavaScript (interfejs strony internetowej jako "konsola" dla drugiego gracza).
- **Backend**: Unity 3D (silnik gry), po��czony za pomoc� WebSocket�w z przegl�dark�.
- **API**: W�asnor�cznie stworzone API do komunikacji mi�dzy gr� a stron� internetow�.
- **Baza danych**: Brak potrzeby przechowywania d�ugotrwa�ych danych, projekt opiera si� na wymianie informacji w czasie rzeczywistym.

## 2. Funkcjonalno�ci
- **Mechanika drzwi**: Gracze musz� wsp�pracowa�, aby otwiera� przej�cia w grze.
- **Podnoszenie przedmiot�w**: Mechanika umo�liwiaj�ca zbieranie wa�nych dla rozgrywki obiekt�w oraz przesuwanie ich w celu rozwi�zania zagadek.

## 3. Problemy i ich rozwi�zania
- **Synchronizacja mi�dzy gr� a stron�**: Trudno�ci z prawid�owym przekazywaniem danych w czasie rzeczywistym rozwi�zano poprzez wdro�enie optymalnych metod WebSocketowych.
- **Kamera gracza**: Niestabilno�� obrot�w kamery podczas intensywnego ruchu zosta�a naprawiona przy u�yciu `Mathf.Clamp` do ograniczenia zakresu patrzenia oraz precyzyjniejszej obs�ugi rotacji osi X i Y.
- **Mechanika skakania**: Kumulacja si�y przy wielokrotnych podskokach rozwi�zana przez resetowanie osi Y pr�dko�ci po wykonaniu skoku.

## 4. Komunikacja
Gra Unity komunikuje si� z serwerem WebSocket, przesy�aj�c zdarzenia za pomoc� event�w, takich jak na przyk�ad pr�ba utworzenia sesji (`GS_tryCreateSession`). Serwer odbiera te zdarzenia, przetwarza je i podejmuje odpowiednie akcje. W przypadku zdarzenia od Unity serwer mo�e utworzy� now� sesj� i zwr�ci� do klienta (gry) wygenerowany token sesji.
Strona WWW korzysta z WebSocket�w do przesy�ania zdarze�, takich jak logowanie do istniej�cej sesji (`WS_tryLoginSession`) czy modyfikowanie stanu element�w (`WS_tryToggleElementState`, `WS_tryRangeElementState`). Serwer weryfikuje token sesji, aby upewni� si�, �e ��danie pochodzi od uprawnionego klienta. Je�li weryfikacja si� powiedzie, serwer przesy�a odpowiedni� wiadomo�� zwrotn� do Unity lub strony WWW, informuj�c o powodzeniu lub niepowodzeniu operacji.
Ca�o�� komunikacji dzia�a w oparciu o zdarzenia WebSocketowe, kt�re pozwalaj� na bie��c� wymian� danych mi�dzy gr�, serwerem i stron� w czasie rzeczywistym. Serwer pe�ni rol� centralnego punktu koordynuj�cego wszystkie interakcje.

## 5. Linki
