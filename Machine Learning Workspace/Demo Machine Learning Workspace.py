#Demo Machine Learning Workspace

#Notebook erstellen
#Compute erstellen
#    Schedule definieren
# Authenticate compute 

# Dependencies
pip install azureml-sdk azureml-widgets

# Code
from azureml.core import Workspace
ws = Workspace.from_config()

# Alternative
from azureml.core import Workspace
ws = Workspace.get(name='aml-workspace',
                   subscription_id='1234567-abcde-890-fgh...',
                   resource_group='aml-resources')

for compute_name in ws.compute_targets:
    compute = ws.compute_targets[compute_name]
    print(compute.name, ":", compute.type)