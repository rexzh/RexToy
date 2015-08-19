#{for i in array[1:5]}
	#{if i<3}
		#{continue}
	#{else}
		#{i}
	#{end}
#{end}