#{for i in array[1:5]}
	#{if i>3}
		#{break}
	#{else}
		#{i}
	#{end}
#{end}