import os
import pymongo
from bson.json_util import loads

client = pymongo.MongoClient('localhost',27017)
client.drop_database("BudgetDB")
db = client.BudgetDB

for filename in os.listdir("data/"):
    with open("data/"+filename) as f:
        jsonstr = f.read()
        print(jsonstr)
        bsonobj = loads(jsonstr)
        collection = db[filename.split(".")[0]]
        collection.insert_many(bsonobj)