-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: localhost
-- Czas generowania: 29 Sty 2023, 13:34
-- Wersja serwera: 10.5.18-MariaDB-0+deb11u1
-- Wersja PHP: 7.4.33

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Baza danych: `transport_system`
--

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `Bus`
--

CREATE TABLE `Bus` (
  `Id` int(11) NOT NULL,
  `Brand` text NOT NULL,
  `Model` text NOT NULL,
  `Number` int(11) NOT NULL,
  `Year` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Zrzut danych tabeli `Bus`
--

INSERT INTO `Bus` (`Id`, `Brand`, `Model`, `Number`, `Year`) VALUES
(65, 'Solaris', 'Urbino 15', 739, 2002),
(66, 'Mercedes', 'Citaro O530', 805, 2012),
(83, 'Solaris', 'Urbino 18 ', 777, 2019),
(92, 'Solaris', 'Urbino 18', 776, 2019),
(93, 'Solaris', 'Urbino 18', 778, 2019),
(94, 'Solaris', 'Urbino 12 CNG', 746, 2008),
(95, 'Solaris', 'Urbino 18', 775, 2019),
(96, 'Solaris', 'Urbino 18', 777, 2019),
(97, 'Solaris', 'Urbino 18', 777, 2019),
(98, 'Solaris', 'Urbino 12 Electric', 100, 2020),
(103, 'Testowy', 'Test', 555, 1980),
(104, 'Testowy', 'Test', 555, 1980),
(105, 'Test', 'TestAPI', 555, 1977),
(106, 'Test', 'TestAPI', 567, 1980),
(107, 'Test', 'TestAPI', 567, 1980),
(108, 'Test', 'TestAPI', 567, 1980),
(109, 'Test', 'TestAPI', 567, 1980),
(110, 'Test', 'TestAPI', 567, 1980),
(111, 'Test', 'TestAPI', 567, 1980),
(112, 'Test', 'TestAPI', 567, 1980);

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `Duty`
--

CREATE TABLE `Duty` (
  `DutyID` int(11) NOT NULL,
  `BusID` int(11) NOT NULL,
  `LineID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `Line`
--

CREATE TABLE `Line` (
  `LineId` int(11) NOT NULL,
  `LineNumber` int(11) NOT NULL,
  `StopAID` int(11) NOT NULL,
  `StopBID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Zrzut danych tabeli `Line`
--

INSERT INTO `Line` (`LineId`, `LineNumber`, `StopAID`, `StopBID`) VALUES
(1, 1, 1, 2),
(2, 8, 3, 5);

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `Stops`
--

CREATE TABLE `Stops` (
  `StopId` int(11) NOT NULL,
  `StopName` text NOT NULL,
  `City` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Zrzut danych tabeli `Stops`
--

INSERT INTO `Stops` (`StopId`, `StopName`, `City`) VALUES
(1, 'Lisa Kuli 02', 'Rzeszów'),
(2, 'Centrum Komunikacji Łańcut', 'Łańcut'),
(3, 'kard. K. Wojtyły', 'Rzeszów'),
(4, 'Matysowska', 'Rzeszów'),
(5, 'Matuszczaka', 'Rzeszów'),
(6, 'Jasionka - Port Lotniczy', 'Rzeszów'),
(7, 'Łukasiewicza', 'Rzeszów'),
(8, 'Hermanowa Przylasek', 'Rzeszów'),
(9, 'Królka', 'Rzeszów'),
(10, 'Lwowska Szpital', 'Rzeszów'),
(11, 'Sikroskiego MPGK', 'Rzeszów'),
(12, 'Dworzec PKP Łańcut', 'Łańcut'),
(13, 'Kasprowicza Dworzec Lokalny', 'Rzeszów'),
(14, 'Olbrachta', 'Rzeszów'),
(15, 'Wita Stwosza', 'Rzeszów'),
(17, 'test', 'dupad');

--
-- Indeksy dla zrzutów tabel
--

--
-- Indeksy dla tabeli `Bus`
--
ALTER TABLE `Bus`
  ADD PRIMARY KEY (`Id`);

--
-- Indeksy dla tabeli `Duty`
--
ALTER TABLE `Duty`
  ADD PRIMARY KEY (`DutyID`);

--
-- Indeksy dla tabeli `Line`
--
ALTER TABLE `Line`
  ADD PRIMARY KEY (`LineId`),
  ADD KEY `StopBID` (`StopBID`),
  ADD KEY `StopAID` (`StopAID`);

--
-- Indeksy dla tabeli `Stops`
--
ALTER TABLE `Stops`
  ADD PRIMARY KEY (`StopId`);

--
-- AUTO_INCREMENT dla zrzuconych tabel
--

--
-- AUTO_INCREMENT dla tabeli `Bus`
--
ALTER TABLE `Bus`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=113;

--
-- AUTO_INCREMENT dla tabeli `Duty`
--
ALTER TABLE `Duty`
  MODIFY `DutyID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT dla tabeli `Line`
--
ALTER TABLE `Line`
  MODIFY `LineId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT dla tabeli `Stops`
--
ALTER TABLE `Stops`
  MODIFY `StopId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=18;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
