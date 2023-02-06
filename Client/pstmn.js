//On mac server url is https://localhost:7198
//On Linux http://localhost:5231


//#region Login Page / User Functions 


function superEnter() {
    let username = document.getElementById("userName");
    let password = document.getElementById("password");
   enterSend(username);
   enterSend(password);
}

function searchEnter() {
    let search = document.getElementById("searchInput");
    search.addEventListener("keypress", (key) => {
        if(key.key == "Enter")
        {
            document.getElementById("searchButton").click();
        }
       });
}

function enterSend(element) {
    element.addEventListener("keypress", (key) => {
        if(key.key == "Enter") 
        {
            document.getElementById("loginButton").click();
        }
    });  
}

async function login(username, password){
     username = document.getElementById("userName");
     password = document.getElementById("password");
    let userData = {"userName" : username.value,
                "password" : password.value
                };
    let params = {
        method : "POST",
        headers : {"Content-Type" : "application/json"},
        body : JSON.stringify(userData)
    };
    let resp = await fetch("http://127.0.0.1:5231/api/users/", params);
    let data = await resp.text();
    sessionStorage.setItem("token", data);
    if(resp.status == 200)
    {
        window.location.replace("./Homepage.html");
    }
    else if(resp.status == 401)
    {
        let loginErrorSpan = document.getElementById("loginError");
        loginErrorSpan.innerText = data;
    }
    else 
    {
        let loginErrorSpan = document.getElementById("loginError");
        loginErrorSpan.innerText = "Username or password are wrong, please try aagain.";
    }
    }


//Token Handler
let tokenData = sessionStorage.getItem("token");
let headersOptions = { "Content-Type" : "application/json", "Authorization" : "Bearer " +  tokenData};


async function logout() {
    let params = {
        method : "DELETE",
        headers : headersOptions,
    };
    let resp = await fetch("http://127.0.0.1:5231/api/users", params);
    let data = await resp.text();
    sessionStorage.removeItem("token");
    window.location.replace("./login.html");
}

    async function UserFunction(){
        let params1 = {
            method : "GET",
            headers : headersOptions,
        };
        let params2 = {
            method : "POST",
            headers : headersOptions,
            body : JSON.stringify(tokenData)
        };
        //resp1 - ActionsLeft
        let resp1 = await fetch("http://127.0.0.1:5231/api/users", params1);
        let data1 = await resp1.text();
        if(resp1.status == 200 && data1 > 0)
        {
        }
        else
        {
            window.location.replace("./login.html");
        }
        if(resp1.status == 401 && data1 > 0 || resp1.status == 401 && data1 <= 0)
        {
            window.location.replace("./login.html");
        }
        //resp2 - User Info Display (User full name and actions left)
        let userName = document.createElement("div");
        let userActionsLeft = document.createElement("div");
        let homepage = document.getElementById("helloHomepage");
        let hdr = document.getElementById("hdr");
        let resp2 = await fetch("http://127.0.0.1:5231/api/users/UserInfo", params2);
        let data2 = await resp2.json();
        userName.innerHTML = data2.currentUserName;
        userName.style.fontSize = "18px"; 
        userName.style.textAlign = "left";
        userName.style.top = "1";
        userActionsLeft.innerHTML = `Number of actions left: ${data2.currentuserActionsleft}`;
        userActionsLeft.style.fontSize = "18px";
        userActionsLeft.style.textAlign = "left";
        userActionsLeft.style.top = "1";
        hdr.appendChild(userName);
        hdr.appendChild(userActionsLeft);
        if(window.document.URL.includes("Homepage"))
        {
            homepage.innerHTML = `Hello, ${data2.currentUserFullName}`;
        }
        }


         function searchBar() {
            let userInput = document.getElementById("searchInput");
            let trim = userInput.value.trim();
            let search= trim.replace("%20", " ");
            if(search.length != 0 )
            {
                window.location.replace(`./SearchResult.html?q=${search}`);
            }
            else {
                let searchSpan = document.getElementById("cannot");
                searchSpan.innerText = "Must enter a value.";
            }
        }


        async function fetchSearchResults() {
            let params = {
                method : "GET",
                headers : headersOptions,
            };
            let resp = await fetch("http://127.0.0.1:5231/api/employees", params);
            let employeesData = await resp.json();

            let resp1 = await fetch("http://127.0.0.1:5231/api/departments", params);
            let departmentsData = await resp1.json();
            
            let empShifts = await fetch(`http://localhost:5231/api/shifts/GetEmpShifts`, params);
            let empShiftsData = await empShifts.json();


            let urlQuery = window.location.href.split('=').reverse()[0];
            let trim = urlQuery.trim();
            let search= trim.replace("%20", " ");


            let employeeTable = document.getElementById("tBody");

            
            function turnNameToID() {
                let empInDepartments = departmentsData.filter(department => department.departmentName.includes(search));
                let dep = [];
                if(empInDepartments != null)
                {
                 dep = empInDepartments.map(department => department.id);
                }    
                return dep;            
            }
        
           employeesData.forEach(employee => {
            let depSearch = turnNameToID();
            if( search.includes(employee.firstName) || search.includes(employee.lastName) || depSearch.includes(employee.departmentID))
            {
                let tableRow = document.createElement("tr");
                let employeeID = document.createElement("td");
                let employeeName = document.createElement("td");
                let departmentID = document.createElement("td");
                let startYear = document.createElement("td");
                let assignedShifts = document.createElement("td");
                let editEmp = document.createElement("td");
                let updateButton = document.createElement("a");
                let deleteEMp = document.createElement("button");
                let assignEmp = document.createElement("a");
        
                
                employeeID.innerText = employee.id;
                employeeName.innerText = employee.firstName + " " + employee.lastName;
                departmentsData.forEach(department => {
                    if(department.id == employee.departmentID)
                    {
                        departmentID.innerText = department.departmentName;
                    }
                });
                startYear.innerText = employee.startYear;
        
        
                empShiftsData.forEach(shift => {
                    if(shift.employeeID == employee.id)
                    {
                        let empShift = document.createElement("td");
                        shiftDate = shift.shiftDate;
                        shiftStart = shift.startTime;
                        ShiftEnd = shift.endTime;
                        empShift.innerText = `${shiftDate} From ${shiftStart} Until ${ShiftEnd}`;
                        empShift.style.display = "block";
                        assignedShifts.appendChild(empShift);
                    }
            
                });
        
        
                updateButton.href = "./EditEmployee.html?employeeId=" + employee.id;
                updateButton.innerText = "Edit";
                updateButton.style.display = "inline-block";
        
                assignEmp.href = "./AssignShift.html?employeeId=" + employee.id;
                assignEmp.innerText = "Add Shift";
                assignEmp.style.display = "block";
        
        
                deleteEMp.innerText = "Delete";   
                deleteEMp.id = "deleteEmpButton";
                deleteEMp.value = employee.id;
                deleteEMp.addEventListener("click", function() {
                    deleteEmployee(employee.id);
                });
        
        
        
        
                editEmp.appendChild(assignEmp);
                editEmp.appendChild(updateButton);
                editEmp.appendChild(deleteEMp);
                tableRow.appendChild(employeeID);
                tableRow.appendChild(employeeName);
                tableRow.appendChild(departmentID);
                tableRow.appendChild(startYear);
                tableRow.appendChild(assignedShifts);
                tableRow.appendChild(editEmp);
                employeeTable.appendChild(tableRow);

            }
           });
            
        }
//#endregion


//#region Employees



async function getEmployees() {
    let requestParams = {
      method: 'GET',
      credantials : 'include',
      headers : headersOptions
    };

    let resp = await fetch("http://127.0.0.1:5231/api/employees",requestParams);
    let data = await resp.json();

    let departmentsResp = await fetch('http://localhost:5231/api/departments', 
    {   method : 'GET',
        headers : headersOptions
    });
    let departmentsData = await departmentsResp.json();



    let empShifts = await fetch(`http://localhost:5231/api/shifts/GetEmpShifts`, {
        method : "GET",
        headers : headersOptions
});
    let empShiftsData = await empShifts.json();
    loader(empShifts);

    let employeeTable = document.getElementById("tBody");
    employeeTable.innerHTML = "";
    data.forEach(employee => {
        let tableRow = document.createElement("tr");
        let employeeID = document.createElement("td");
        let employeeName = document.createElement("td");
        let departmentID = document.createElement("td");
        let startYear = document.createElement("td");
        let assignedShifts = document.createElement("td");
        let editEmp = document.createElement("td");
        let updateButton = document.createElement("a");
        let deleteEMp = document.createElement("button");
        let assignEmp = document.createElement("a");


        employeeID.innerText = employee.id;
        employeeName.innerText = employee.firstName + " " + employee.lastName;
        departmentsData.forEach(department => {
            if(department.id == employee.departmentID)
            {
                departmentID.innerText = department.departmentName;
            }
        });
        startYear.innerText = employee.startYear;


        empShiftsData.forEach(shift => {
            if(shift.employeeID == employee.id)
            {
                let empShift = document.createElement("td");
                shiftDate = shift.shiftDate;
                shiftStart = shift.startTime;
                ShiftEnd = shift.endTime;
                empShift.innerText = `${shiftDate} From ${shiftStart} Until ${ShiftEnd}`;
                empShift.style.display = "block";
                assignedShifts.appendChild(empShift);
            }
    
        });


        updateButton.href = "./EditEmployee.html?employeeId=" + employee.id;
        updateButton.innerText = "Edit";
        updateButton.style.display = "inline-block";

        assignEmp.href = "./AssignShift.html?employeeId=" + employee.id;
        assignEmp.innerText = "Add Shift";
        assignEmp.style.display = "block";


        deleteEMp.innerText = "Delete";   
        deleteEMp.id = "deleteEmpButton";
        deleteEMp.value = employee.id;
        deleteEMp.addEventListener("click", function() {
            deleteEmployee(employee.id);
        });




        editEmp.appendChild(assignEmp);
        editEmp.appendChild(updateButton);
        editEmp.appendChild(deleteEMp);
        tableRow.appendChild(employeeID);
        tableRow.appendChild(employeeName);
        tableRow.appendChild(departmentID);
        tableRow.appendChild(startYear);
        tableRow.appendChild(assignedShifts);
        tableRow.appendChild(editEmp);
        employeeTable.appendChild(tableRow);
    });
    
}

async function displayEmployeeInfo() {
    let idFromUrl = window.location.href.split('=').reverse()[0];
    let employeeID = document.getElementById("employeeID");
    let firstName = document.getElementById("firstNameU");
    let lastName = document.getElementById("lastNameU");
    let startYear = document.getElementById("startYearU");
    let departmentID = document.getElementById("depSelectVal");

    let resp = await fetch(`http://localhost:5231/api/employees/${idFromUrl}`, {
        method : "GET",
        headers : headersOptions,
    });
        let data = await resp.json();
        console.log(data);
        employeeID.value = data.id;
        firstName.value = data.firstName;
        lastName.value = data.lastName;
        startYear.value = data.startYear;
        departmentID.value = data.departmentID;
}





async function newEmployeeAdd() {
    let firstName = document.getElementById("firstName").value;
    let lastName = document.getElementById("lastName").value;
    let startYear = document.getElementById("startYear").value;
    let departmentID = document.getElementById("depSelect").value;
    let employeeObj = {"firstName" : firstName,
                        "lastName" : lastName,
                        "startYear" : startYear,
                        "departmentID" : departmentID };

    let resp = await fetch("http://localhost:5231/api/employees", {
                            method : "POST",
                            headers : headersOptions,
                            body : JSON.stringify(employeeObj),
    });
    let data = await resp.text();
    let employeeAddedSpan = document.getElementById("employeeAdded");
    employeeAddedSpan.innerText = data;
    getEmployees();
}

async function updateEmployee() {
    let employeeId = document.getElementById("employeeID").value;
    let firstName = document.getElementById("firstNameU").value;
    let lastName = document.getElementById("lastNameU").value;
    let startYear = document.getElementById("startYearU").value;
    let departmentID = document.getElementById("depSelect").value;
    let employeeUpdate = {
        "firstName" : firstName,
        "lastName" : lastName, 
        "startYear" : startYear, 
        "departmentID" : departmentID 
    };

    let resp = await fetch("http://localhost:5231/api/employees/" + employeeId, {
                            method : "PUT",
                            headers : headersOptions,
                            body : JSON.stringify(employeeUpdate)
    })
    let data = await resp.text();
    let employeeUpdatedSpan = document.getElementById("employeeUpdated");
    employeeUpdatedSpan.innerText = data;
}


 async function deleteEmployee(employeeId) {
    let resp = await fetch(`http://localhost:5231/api/employees/${employeeId}`, {
                            method : "DELETE",
                            headers : headersOptions
    });
    let data = await resp.text();
        let employeeDeletedSpan = document.getElementById("employeeDeletedSpan");
        employeeDeletedSpan.innerText = data;
    getEmployees();
}


async function employeeSelect(employeeSelect) {
  
    let resp1 = await fetch('http://localhost:5231/api/employees', 
    {   method : 'GET',
        headers : headersOptions
    });
    let data1 = await resp1.json();
    data1.forEach(emp => {
        let employeeOption = document.createElement("option");
        employeeOption.textContent = `${emp.firstName}  ${emp.lastName}`;
        employeeOption.value = emp.id;
        employeeSelect.appendChild(employeeOption);
    });
}




//#endregion

//#region Departments


async function getDepartments() {

    let departmentsTbody = document.getElementById("departmentsTbody");

    departmentsTbody.innerHTML = "";

    let resp = await fetch('http://localhost:5231/api/departments', 
    {   method : 'GET',
        headers : headersOptions
    });
    let data = await resp.json();



    let requestParams = {
        method: 'GET',
        credantials : 'include',
        headers : headersOptions
      };
  
      let employeeResp = await fetch("http://127.0.0.1:5231/api/employees",requestParams);
      let employeeData = await employeeResp.json();
      const emptyDep = [];

      loader(resp);


    data.forEach(dp => {
        let tableRow = document.createElement("tr");
        let departmentID = document.createElement("td");
        let departmentName = document.createElement("td");
        let departmentManager = document.createElement("td");

        let deleteDep = document.createElement("button");
        deleteDep.innerText = "Delete";  
        deleteDep.style.display = "none"; 
        deleteDep.id = "deleteEmpButton";
        deleteDep.value = dp.id;
        deleteDep.addEventListener("click", function() {
            deleteDepartment(dp.id);
            getDepartments();
        });
   
        
        departmentID.innerText = dp.id;
        departmentName.innerText = dp.departmentName;
        let anyEmployees = false;
        employeeData.forEach(emp => {
            if(emp.id == dp.manager)
            {
                departmentManager.innerText = `${emp.firstName} ${emp.lastName}`;
            }
            if(emp.departmentID ===  dp.id)
            {
                anyEmployees = true;
            }
        });

        if(!anyEmployees)
        {
            emptyDep.push(dp.id);
            emptyDep.filter(Number);
            emptyDep.forEach(element => {
                if(element == deleteDep.value)
                {
                    deleteDep.style.display = "inline-block"; 
                }
                
            });
        }
        let editDepartment = document.createElement("td");
        let editDepartmentLink = document.createElement("a");

        editDepartmentLink.href = "./EditDepartment.html?departmentId=" + dp.id;
        editDepartmentLink.innerText = "Edit";
       

        tableRow.appendChild(departmentID);
        tableRow.appendChild(departmentName);
        tableRow.appendChild(departmentManager);  
        tableRow.appendChild(editDepartment); 
        editDepartment.appendChild(editDepartmentLink);
        editDepartment.appendChild(deleteDep);
        departmentsTbody.appendChild(tableRow);

    });

}

async function departmentSelect() {
    let depSelect = document.getElementById("depSelect");
    let resp = await fetch('http://localhost:5231/api/departments', 
    {   method : 'GET',
        headers : headersOptions
    });
    let data = await resp.json();
    data.forEach(dp => {
        let departmentOption = document.createElement("option");
        departmentOption.textContent = dp.departmentName;
        departmentOption.value = dp.id;
        depSelect.appendChild(departmentOption);
    });
}


async function chooseNewDepManager() {
    let departmentManager = document.getElementById("departmentManager");
    let empOptions = await employeeSelect(departmentManager);
    
    
}

async function addDepartment() {
    let departmentName = document.getElementById("departmentName").value;

    let departmentManager = document.getElementById("departmentManager").value;
    
    let addedDepSpan = document.getElementById("addedDep");

    let departmentData = {
        "departmentName" : departmentName, 
        "manager" : +departmentManager
    };
    let postParams = {
        method : "POST", 
        mode : "cors", 
        cache : "no-cache",
        headers : headersOptions,
        body : JSON.stringify(departmentData)
    }

    let resp = await fetch("http://localhost:5231/api/departments", postParams);
    let data = await resp.text();
    getDepartments();
    getEmployees();
    addedDepSpan.innerText = data;
}


async function displayDepartmentInfo() {
    let idFromUrl = window.location.href.split('=').reverse()[0];
    let departmentID = document.getElementById("departmentIdU");
    let departmentName = document.getElementById("departmentNameU");
    let departmentManager = document.getElementById("departmentManagerU");
    let empOptions = await employeeSelect(departmentManager);
    
   

    let resp = await fetch(`http://localhost:5231/api/departments/${idFromUrl}`, {
        method : "GET",
        headers : headersOptions,
    });

        let data = await resp.json();

        departmentID.value = data.id;
        departmentName.value = data.departmentName;
        departmentManager.value = data.manager;
}


async function updateDepartment() {
    let departmentID = document.getElementById("departmentIdU").value;
    let departmentName = document.getElementById("departmentNameU").value;
    let departmentManager = document.getElementById("departmentManagerU").value;

    let updateData = {
        "departmentName" : departmentName, 
        "manager" : departmentManager
    };
    let putParams = {
        method : "PUT",
        mode : "cors", 
        cache : "no-cache", 
        headers : headersOptions,
        body : JSON.stringify(updateData)
    };
    let resp = await fetch("http://localhost:5231/api/departments/" + departmentID, putParams);
    let data = await resp.text();
    let updatedDepartment = document.getElementById("updatedDep");
    updatedDepartment.innerText = data;
}

async function deleteDepartment(departmentID) {
    let resp = await fetch("http://localhost:5231/api/departments/" + departmentID, {
                            method : "DELETE",
                            headers : headersOptions
    });
    let data = await resp.text();
    let departmentDeletedSpan = document.getElementById("departmentDeletedSpan");
    departmentDeletedSpan.innerText = data;
}



//#endregion

//#region  Shifts

async function getShifts() {
    let resp = await fetch(`http://localhost:5231/api/shifts/`, {
        method : "GET",
        headers : headersOptions
});
    let data = await resp.json();
    loader(resp);
    
    let employees = await fetch(`http://localhost:5231/api/shifts/GetEmpShifts`, {
        method : "GET",
        headers : headersOptions
});
    let employeesData = await employees.json();
    
    let shiftsTBody = document.getElementById("shiftTBody");
    data.forEach(shift => {
        let tableRow = document.createElement("tr");
        let shiftID = document.createElement("td");
        let shiftDate = document.createElement("td");
        let startTime = document.createElement("td");
        let endTime = document.createElement("td");
        let date = new Date(shift.date);
        let employeeTd = document.createElement("td");

        
        shiftID.innerText = shift.id;
        shiftDate.innerText =  date.toLocaleDateString(); 
        startTime.innerText = shift.startTime;
        endTime.innerText = shift.endTime;

        tableRow.appendChild(shiftID);
        tableRow.appendChild(shiftDate);
        tableRow.appendChild(startTime);
        tableRow.appendChild(endTime);


        employeesData.forEach(employee => {
            if(shift.id === employee.id)
            {
            let editEmpLink = document.createElement("a");
            editEmpLink.style.display = "unset";
            editEmpLink.href = `./EditEmployee.html?userId=${employee.employeeID}`
            editEmpLink.innerText = employee.employeeName;
            employeeTd.appendChild(editEmpLink);
            tableRow.appendChild(employeeTd);
            }
        });



        shiftsTBody.appendChild(tableRow);
    });
}


async function addShift() {
    let shiftDate = document.getElementById("shiftDate").value;
    let startTime = document.getElementById("startTime").value;
    let endTime = document.getElementById("endTime").value;
    let shiftObj = {
        Date : shiftDate,
        startTime : startTime,
        endTime : endTime
    }
    let resp = await fetch("http://localhost:5231/api/shifts", {
        method : "POST",
        headers : headersOptions,
        body : JSON.stringify(shiftObj),
});
let data = await resp.text();
if(resp.status == 200) 
{
    let addedShift = document.getElementById("addedShift");
    addedShift.innerText = data;
}    
}



async function assignShiftSelect() {
    let shiftsSelect = document.getElementById("shiftsSelect");
    let empSelect = document.getElementById("employeeSelect");
    let resp = await fetch('http://localhost:5231/api/shifts', 
    {   method : 'GET',
        headers : headersOptions
    });
    let data = await resp.json();
    data.forEach(shift => {
        let shiftOption = document.createElement("option");
        let date = new Date(shift.date);
        shiftOption.textContent = `${date.toLocaleDateString()} From ${shift.startTime} Until ${shift.endTime}`;
        shiftOption.value = shift.id;
        shiftsSelect.appendChild(shiftOption);
    });

     let empOpt = await employeeSelect(empSelect);
    for(let option of empSelect){
        let idFromUrl = window.location.href.split('=').reverse()[0];
        if(option.value == idFromUrl)
        {
            option.selected = true;
            break;
        }
    
    }
}



async function assignShift() {
    let employeeSelect = document.getElementById("employeeSelect").value;
    let shiftsSelect = document.getElementById("shiftsSelect").value;

    let shiftObj = {
        employeeID : employeeSelect,
        shiftID : shiftsSelect
    };

    let resp = await fetch("http://localhost:5231/api/shifts/EmpToShift", {
        method : "POST",
        headers : headersOptions,
        body : JSON.stringify(shiftObj),
    });

    let data = await resp.text();
        if(resp.status == 200) 
    {
        let assignedShift = document.getElementById("assignedShift");
        assignedShift.innerText = data;
    }        
}


//#endregion


