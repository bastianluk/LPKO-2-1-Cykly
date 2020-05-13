# Dokumentace

Dokumentace k generátoru pro příklad `Orientované grafy bez krátkých orientovaných cyklů`[link](https://kam.mff.cuni.cz/~balko/lpko1920/ukolPrakticky.pdf) napsaném v jazyce C#.

## Sestavení

K sestavení otevřete projekt ve vámi zvoleném IDE podporující C# a spusťte build.

## Ovládání

Generátoru se jako argument pošlu path ke vstupnímu souboru a on vygeneruje souboru `vygenerovane_lp.mod` ve stejne složce, jako je samotné `.exe`, ve kterém bude odpovídající lineární program, na který stačí pustit `glpsol -m vygenerovane_lp.mod`.

## Lineární program

Příklad výstupu:
```c
set Nodes := 0..5;
set Edges, within Nodes cross Nodes;
param weight{(i,j) in Edges};
var isRemoved{(i,j) in Edges}, binary;
minimize total: sum{(i,j) in Edges} weight[i,j] * isRemoved[i,j];
s.t. condition_circle4{i in Nodes, j in Nodes, k in Nodes, l in Nodes: not(i == j or j == k or k == l or l == i)}:
  ( if ((i,j) in Edges and (j,k) in Edges and (k,l) in Edges and (l,i) in Edges) then (isRemoved[i,j] + isRemoved[j,k] + isRemoved[k,l] + isRemoved[l,i]) else 1 ) >= 1;
s.t. condition_circle3{i in Nodes, j in Nodes, k in Nodes: not(i == j or j == k or k == i)}:
  ( if ((i,j) in Edges and (j,k) in Edges and (k,i) in Edges) then (isRemoved[i,j] + isRemoved[j,k] + isRemoved[k,i]) else 1 ) >= 1;
solve;
printf "#OUTPUT: %d\n", sum{(i,j) in Edges} weight[i,j] * isRemoved[i,j];
for {(i,j) in Edges: i != j}
{
  printf (if isRemoved[i,j] = 1 then "%d --> %d\n" else ""), i, j;
}
printf "#OUTPUT END\n";
data;
param : Edges : weight := 5 1 15
                          4 2 30
                          3 4 36
                          1 0 41
                          0 4 15
                          1 2 40
                          3 5 21
                          0 5 26
                          5 2 37
                          0 2 8
                          2 3 21
                          4 5 23
                          0 3 29;
end;
```

Program se snaží minimalizovat funkci `sum{(i,j) in Edges} weight[i,j] * isRemoved[i,j];` - tedy hledá hrany, které po odebrání zaručí splnění podmínek a součet jejich vah je nejmenší.

Podmínky jsou triviálně pro čtveřice (respektive trojice) vrcholů, mezi kterými pokud je cyklus délky 4 (3), tj. vede hrana z A do B, B do C, C do D a z D do A, tak alespoň jedna z těch hran musí být označena jako odebraná ve výsledném grafu.

### Nemá optimální řešení

Pokud řešič nenajde řešení, tak nelze odebrat hrany z vstupního grafu tak, aby se v něm následně nenacházeli krátké cykly (tedy cykly délky 3 nebo 4).
