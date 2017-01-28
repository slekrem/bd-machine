var getOptions = function(titleText,xAxesText, yAxesText){
	return {
		responsive: true,
		title: {
			display:true,
			text: titleText
		},
		tooltips: {
			mode: 'label',
			callbacks: { }
		},
		hover: {
			mode: 'dataset'
		},
		scales: {
			xAxes: [{
				display: true,
				scaleLabel: {
					display: true,
					labelString: xAxesText
				}
			}],
			yAxes: [{
				display: true,
				scaleLabel: {
					display: true,
					labelString: yAxesText
				},
			}]
		}
	};
};

var options = {
		responsive: true,
		title: {
			display:true,
			text:'Host'
		},
		tooltips: {
			mode: 'label',
			callbacks: { }
		},
		hover: {
			mode: 'dataset'
		},
		scales: {
			xAxes: [{
				display: true,
				scaleLabel: {
					display: true,
					labelString: 'Monat'
				}
			}],
			yAxes: [{
				display: true,
				scaleLabel: {
					display: true,
					labelString: 'Anzahl'
				},
			}]
		}
	};