# Symulacja Tomografu Komputerowego (CT) - projekt

Aplikacja prezentuje uproszczoną symulację działania tomografu komputerowego z wykorzystaniem **transformacji Radona** oraz **odwrotnej transformacji Radona**. Projekt zawiera również moduł do odczytu i zapisu plików **DICOM**.

Interfejs został zbudowany w **Streamlit**, co pozwala uruchomić program w formie aplikacji webowej.

---

## Zastosowany model tomografu
W projekcie wykorzystano **równoległy model tomografu** (parallel-beam CT).

---

## Technologie i biblioteki
Projekt napisano w **Pythonie**, używając m.in.:

- streamlit
- numpy
- matplotlib
- scikit-image
- pydicom
- PIL
- os

Pełna lista znajduje się w `requirements.txt`.

---

## Uruchomienie projektu

1. **Stwórz i aktywuj środowisko wirtualne**
   ```
   python -m venv venv
   venv\Scripts\activate
   ```

2. **Zainstaluj wymagania**
   ```
   pip install -r requirements.txt
   ```

3. **Utwórz katalogi na obrazy**
   ```
   images/
   dicoms/
   ```
które nie są częścią repozytorium i muszą zostać utworzone ręcznie.
Folder images/ służy do przechowywania zwykłych obrazów (np. .jpg, .png), a dicoms/ do przechowywania plików medycznych DICOM.

4. **Uruchom aplikację**
   ```
   streamlit run main.py
   ```

---

## Funkcjonalności aplikacji

### Symulacja CT
- generowanie sinogramu,
- rekonstrukcja obrazu,
- podgląd: wejściowy → sinogram → wynikowy.

### Obsługa plików DICOM
- odczyt `.dcm`,
- edycja danych pacjenta,
- zapis do nowego pliku.
