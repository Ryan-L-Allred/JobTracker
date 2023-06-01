import React from "react";
import { Card, CardBody } from "reactstrap";
import { Navigate, useNavigate } from "react-router-dom"
//import { getAllRoles } from "../modules/roleManager";

const Role = ({ role }) => {
    const navigate = useNavigate();

    return (
        <Card>
            <CardBody>
                <section class="text-center">
                    <h3 class="roleTitle">Role</h3>
                     <div class="roleDetails">
                     <ul>
                        <li>Title: {role.title}</li>
                        <li>Company: {role.company}</li>
                        <li>Location: {role.location}</li>
                        <li>Skills: {role.skills}</li>
                        <li>Rejected: {role.isRejected}</li>
                        <li>Accepted: {role.isAccepted}</li>
                        <li>Interview: {role.GotInterview}</li>
                        <li>Experience Level: {role?.experienceLevel?.name}</li>
                        <li>JobType: {role?.jobType?.name}</li>
                        <li>Job Site: {role?.jobSite?.name}</li>
                    </ul>
                    </div>   
                </section>
            </CardBody>
        </Card>
    )
}

export default Role;