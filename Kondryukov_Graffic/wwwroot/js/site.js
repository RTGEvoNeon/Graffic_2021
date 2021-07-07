// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.


am4core.ready(function() {

// Themes begin
am4core.useTheme(am4themes_animated);
// Themes end

var chart = am4core.create("chartdiv", am4charts.XYChart);
    chart.data = MyJson;
// Create axes
var dateAxis = chart.xAxes.push(new am4charts.DateAxis());
    dateAxis.renderer.minGridDistance = 50;
    dateAxis.renderer.grid.template.location = 0;
    dateAxis.minZoomCount = 5;

    dateAxis.groupData = true;
    dateAxis.groupCount = 500;


	function createAxisAndSeries(field, name, opposite, bullet) {
		var valueAxis = chart.yAxes.push(new am4charts.ValueAxis());
		//if (chart.yAxes.indexOf(valueAxis) != 0) {
		//	valueAxis.syncWithAxis = chart.yAxes.getIndex(0);
		//}

		var series = chart.series.push(new am4charts.LineSeries());
		series.dataFields.dateX = "date";
		series.dataFields.valueY = field;
		series.tooltipText = "{name}: [bold]{valueY}[/]";
		series.name = name;
		series.tooltip.pointerOrientation = "vertical";

		// width of lines and opacity
		series.strokeWidth = 2;
		series.fillOpacity = 0.5;

		//series.yAxis = valueAxis;
		//series.tensionX = 0.8;
		//series.showOnInit = true;

		var interfaceColors = new am4core.InterfaceColorSet();
	}

	createAxisAndSeries("Sberbank", "Sberbank", true, "circle");
	createAxisAndSeries("Tinkoff", "Tinkoff", true, "circle");
	createAxisAndSeries("CentralBank", "CentralBank", true, "circle");
	createAxisAndSeries("Cash", "Cash", true, "circle");
	createAxisAndSeries("allMoney", "allMoney", true, "circle");

//var valueAxis = chart.yAxes.push(new am4charts.ValueAxis());

//// Create series
//var series = chart.series.push(new am4charts.LineSeries());
//series.dataFields.valueY = "value";
//series.dataFields.dateX = "date";
//series.tooltipText = "{value}"

//series.tooltip.pointerOrientation = "vertical";

//chart.cursor = new am4charts.XYCursor();
//chart.cursor.snapToSeries = series;
//chart.cursor.xAxis = dateAxis;

//chart.scrollbarY = new am4core.Scrollbar();
	// Add legend
	chart.legend = new am4charts.Legend();
	// Add cursor
	chart.cursor = new am4charts.XYCursor();
	// generate some random data, quite different range
	var scrollbarX = new am4core.Scrollbar();
	scrollbarX.marginBottom = 20;
	chart.scrollbarX = scrollbarX;

}); // end am4core.ready()
