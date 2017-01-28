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