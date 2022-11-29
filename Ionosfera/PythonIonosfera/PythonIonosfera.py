!pip install --upgrade pip
!pip install numpy pandas seaborn matplotlib empiricaldist statsmodels sklearn pyjanitor

import json
import urllib.request
import empiricaldist
import janitor
import matplotlib.pyplot as plt
import numpy as np
import pandas as pd
import scipy.stats
import seaborn as sns
import sklearn.metrics
import statsmodels.api as sm
import statsmodels.formula.api as smf
import statsmodels.stats as ss

"""#retrieve the scintillation stations/instruments list

"""

station='tuj2o'  #change the station code for retrieve the parameters from other stations

#retireve the parameters
since_date='2018-12-11'#YYYY-MM-DD
until_date='2018-12-12'
since_hour='00:00:00'#HH:MM:SS
until_hour='23:00:00'
url='http://ws-eswua.rm.ingv.it/ais.php/records/{}_auto?filter=dt,bt,{}%20{},{}%20{}&include+dt,{}&order=dt'.format(station,since_date,since_hour,until_date,until_hour,station)
webURL=urllib.request.urlopen(url)
parameter=json.loads(webURL.read()) # parameter is a dictionary with the stations/instruments list
data = parameter["records"]

#usar alguna estructura de datos
date_template = '{}-{}-{}'
since_year = 2009
until_year = 2020
for year in range(since_year,until_year+1):
  for month in range(1,12):
    since_date = date_template.format(year,month,1)#YYYY-MM-DD
    until_date=date_template.format(year,month+1,1)
    since_hour='00:00:00'#HH:MM:SS
    until_hour='23:00:00'
    url='http://ws-eswua.rm.ingv.it/ais.php/records/{}_auto?filter=dt,bt,{}%20{},{}%20{}&include+dt,{}&order=dt'.format(station,since_date,since_hour,until_date,until_hour,station)
    webURL=urllib.request.urlopen(url)
    parameter=json.loads(webURL.read()) # parameter is a dictionary with the stations/instruments list
    for record in parameter["records"]:#consume toda la ram
      data.append(record)

none = 0
registros = 0
total = 0
for reg in data:
  if reg["fof2"]:
    registros = registros + 1
  else: 
    none = none + 1
  total = total + 1

print(registros, none, total)

print(data[1])