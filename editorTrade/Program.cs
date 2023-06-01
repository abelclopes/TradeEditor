// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;

var loadTrade = new LoadTrade();
var trades = await loadTrade.LerTradeConfig("./../.././../../TraderConfig.txt");

var gerar = new GeraExcel(trades.tradeConfig);

gerar.GerarCSV(trades.traders);