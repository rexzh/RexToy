#{let a = -5}
#{if a>0:}
	a>0
#{:else}
	#{:if a==0:}
		a=0
	#{:else:}
		a<0
	#{:end}
#{end}