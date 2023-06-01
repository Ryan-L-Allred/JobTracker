import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { Navigate, useNavigate } from "react-router-dom";
import { getAllRoles } from "../modules/roleManager";

const RoleList = () => {
    const [roles, setRoles] = useState([])
    const navigate = useNavigate();

    const getRoles = () => {
        getAllRoles().then(roles => setRoles(roles));
    }

    useEffect(() => {
        getRoles()
    }, [])

    return (
        <section>
            <h2>Roles Applied</h2>
            {roles.map(role => (
                <section key={role.id} class="text-center">
                    <div class="roleList"><b>Title:</b> {role.title}, <b>Company:</b> {role.company}</div>
                </section>
            ))}
        </section>
    );
}

export default RoleList;