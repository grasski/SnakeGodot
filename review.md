Originální kód: https://github.com/Kureii/clean-code-snake

#### 1. Functionality and Correctness - 2 body:<br>
Hra zdánlivě implementuje základní funkce hry Snake, včetně pohybu, sbírání ovoce a podmínek pro konec hry.
Funkce jsou řádně implementovány, jako například funkce "IsGameOver", která kontroluje kolizi jak se zdí, tak i se samotným tělem hada.

#### 2. Code Clarity and Readability - 1 bod:<br>
Proměnné v kódu mají obecně jasné a výstižné názvy, což napomáhá lepšímu pochopení jejich účelu.
Celková srozumitelnost kódu by však mohla být vylepšena přidáním komentářů. To by bylo obzvlášť užitečné u komplexnějších částí,
jako je např. metoda "CalculateBezierY", která je na takovou hru zbytečná.

#### 3. Code Structure and Modularity - 1 bod:<br>
Logika hry je obsažena v jedné třídě "Program", která na začátku obsahuje příliš mnoho globálních proměnných.
Pro vykreslení jsou použity funkce "DrawPixel" a "DrawBorders", které jsou součástí metod "GameLoop" a "DrawGame".
Jak již bylo naznačeno v předchozím bodě, vyskytuje se zde velice komplexní metoda "CalculateBezierY", která nebyla absolutně pochopena
a to i díky jejím parametrům, které přijímá.
Funkce "UpdateGameState" kontroluje jak pohyb tak i změnu skóre, což by bylo možné rozdělit na dvě funkce.

#### 4. Coding Standards and Conventions - 2 body:<br>
Kód dodržuje základní konvence a standardy formátování jazyka C#.
Autor pro deklaraci číselných proměnných používá klíčové slovo "Int32" namísto běžně používaného "int".
Styl psaní kódu je v celém souboru stejný. 

#### 5. Coding Standards and Conventions - 2 body:<br>
Logika kódu a logika vykreslování (UI) je v celku oddělená.
Co se týká implementace logiky hry v herním enginu Godot, většina kódu byla nahrazena vlastním řešením s drobnými úpravami dle autorova kódu. 

# CELKOVÁ ZNÁMKA: 2 body
