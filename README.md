# ITOReinforcedLearning
Final project for AI technologies


## Projekt U4. Uczenie ze wzmacnianiem
Wykonać program przeznaczony do uczenia się agenta strategii poruszania się w świecie
zadanym jako 2-wymiarowa macierz komórek. Wyrazić problem uczenia się strategii wyboru
akcji jako uczenie ze wzmacnianiem.
Świat ma rozmiar N x N (np. N = 8, 10, 12). W losowych miejscach umieszczone są ściany;
których położenie w trakcie jednego uczenia nie zmienia się. Ściana nie dotyka innej ściany i
może tylko w jednym punkcie dotykać brzegu. Na brzegach świata znajduje się M wyjść (np.
M=1, 3, 5).
Uczenie składa się z serii prób. Próba rozpoczyna się od umieszczenia agenta w losowo
wybranym miejscu klatki i kończy się po m × N krokach (np. m=5, 7, 9) albo gdy agent dotrze
do jednego z wyjść. Agent ma do dyspozycji 4 akcje: Wschód, Zachód, Północ, Południe; jest
ujemnie nagradzany za każdą chwilę pobytu w klatce. Celem nauki jest maksymalizacja
(zdyskontowanej) sumy nagród, których agent może oczekiwać w każdej chwili, czyli za jak
najszybsze docieranie do wyjścia.
Wykonać prosty interfejs graficzny, który w trakcie nauki ma podawać średnią nagrodę z
prób w każdym miejscu, natomiast po nauce (faza aktywnego działania agenta) ma
pokazywać zachowanie agenta.
