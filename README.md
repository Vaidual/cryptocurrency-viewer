# cryptocurrency-viewer
 Simple cryptocurrency monitoring app
 
 
Features:
- Shows the top 10 currencies by CoinCap.
- Clicking on a row in the table (row must be selected first) opens the asset details page. While hovering over the graph, you can see the price change over time. At the bottom - available 10 (can be changed in CryptoDetailsVM) markets to exchange with link navigation where possible.
- Asset search by name or symbol showing top 6 matches.
- Custom simple forward-back navigation.

Bugs/problems:
- Pages load before data is loaded.
- Blink animation does not work on updated asset table rows (table updates every 20s).
- XAxis labels are not shown on chart (should be).
- Formatting does not work on markets table.
