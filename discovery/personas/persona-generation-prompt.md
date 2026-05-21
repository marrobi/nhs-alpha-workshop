# Persona generation prompt (Microsoft 365 Copilot → Researcher)

Use this prompt in the **Researcher** agent inside Microsoft 365 Copilot to generate a set of personas for an UKHSA system/initiative.

- Open Copilot Chat: https://m365.cloud.microsoft/chat/
- Find **Researcher** under **Agents**.
- Microsoft docs: https://learn.microsoft.com/en-us/copilot/microsoft-365/researcher-agent

---

### Objective
Research and create detailed user personas for every role involved in the described system, using real-world UK Health Security Agency (UKHSA) job titles and organisational context. Include both UKHSA/internal public sector roles and external roles (such as members of the public, local authorities, healthcare providers, laboratories, policymakers, and partner agencies).

**Important**: Each persona should be written as if describing a specific individual person, not a generalized role. Use concrete details, specific behaviours, and personal context to make each persona feel real and relatable. Keep each persona to one page. Include enough personas to cover every distinct user type, framework level, and interaction pattern — but no more. If a role doesn't add a meaningfully different perspective, merge it with a similar persona. Provide the output in Markdown format.

> Follow [GDS guidance: Understanding users and their context](https://www.gov.uk/service-manual/agile-delivery/how-the-discovery-phase-works#understanding-users-and-their-context) — learn what users are trying to achieve, understand the wider journey, and consider accessibility, digital skills, and offline channels.

### Persona Requirements

For each identified role, create a comprehensive persona using the following template:

**Persona Template Structure:**
1. **Persona Name & Role**
- Fictional first name 
- Official UKHSA job title (for internal staff) OR role description (for external/public users)
- Position level and directorate/team (e.g., Health Protection, Data & Analytics, Emergency Preparedness, Global Health)
- Reporting structure context (for UKHSA roles) OR relationship to public health system (for external roles)
- Brief personal background (e.g., years in role, prior experience, public health exposure)

2. **Goals & Outcomes**
   - Primary objectives in their role (specific to this individual)
   - Key performance indicators (KPIs) they personally track
   - Success metrics that matter to them
   - Desired outcomes from using the system (in their own words/perspective)

3. **Wants, Needs & Expectations**
   - Their specific daily workflow requirements
   - Information and resources they rely on
   - Their technology expectations and preferences
   - Support and training needs based on their experience level
   - How they prefer to communicate

4. **Biggest Pain Points & Unmet Needs**
   - Their current frustrations and challenges (be specific)
   - Workflow bottlenecks they experience
   - System limitations that affect them personally
   - Time-consuming tasks they wish were automated
   - Specific gaps in current solutions that impact their work

5. **Wider Journey & Touchpoints**
    - Where they sit in the public health lifecycle (e.g., prevention, detection, response, recovery)
    - Organisations they interact with:
        NHS organisations (Trusts, GPs)
        Local authorities / Directors of Public Health
        Laboratories and diagnostic services
        Government departments (DHSC, Cabinet Office)
        International bodies (WHO, ECDC)
    - Offline channels (phone, field visits, paper-based processes)
    - Handoffs and dependencies (e.g., lab → surveillance → response teams)

6. **Additional Context**
   - A typical day-in-the-life for this specific person
   - Their technical proficiency level (with examples)
   - Their digital access — devices, connectivity, confidence online, assisted digital needs
   - Accessibility needs (for all personas, not just patients — staff may also have disabilities or access needs)
   - Their key stakeholder relationships
   - Their decision-making authority (if applicable)
   - Regulatory/compliance considerations they deal with (for staff roles)
   - Personal circumstances (for patients/carers)
   - Their specific workload and time constraints
   - Cultural, linguistic, or socioeconomic factors relevant to them

### Coverage Requirements

1. **Framework Levels**: Ensure personas cover all three framework levels:
   - Strategic/Leadership level
   - Tactical/Management level
   - Operational/Front-line level

2. **Workflow Stages**: Identify and group personas by their primary involvement in workflow stages (e.g., planning, execution, monitoring, reporting, governance)

3. **Complete Ecosystem**: Include all roles that:
   - Directly interact with the system
   - Provide input or dependencies
   - Consume outputs or reports
   - Approve or govern processes
   - Support or maintain the system
   - Are impacted by system outcomes
   - **Non-UKHSA roles such as:**
     - Patients (various demographics, conditions, and accessibility needs)
     - Family members and carers
     - Informal carers
     - Community support workers
     - Social care providers
     - Third-sector organizations
     - External partners and suppliers
     - Volunteers
     - Patient advocacy groups

### Output Format

- **One persona per slide/section**
- **Each persona has a fictional first name** to humanize them (use diverse, representative UK names)
- **Write each persona as if describing a specific individual person**, not a generalized role type
- Use concrete, specific details and examples throughout
- **Group personas by workflow stage** (clearly labeled)
- Use consistent formatting across all personas
- Include realistic UKHSA and relevant organisations context, terminology, and constraints
- Base findings on actual organisational structures and real job roles
- **Provide citations** wherever possible to support claims about:
  - Job roles and responsibilities (link to UKHSA/ NHS job frameworks, e.g., Agenda for Change)
  - Typical challenges and pain points (link to reports, surveys, studies)
  - UKHSA organizational structures and hierarchies
  - Regulatory requirements and standards

### Research Approach

- Reference current UKHSA job frameworks and role definitions for staff roles
- Consider UKHSA organizational hierarchies and typical team structures
- Include UKHSA-specific challenges 
- Reflect realistic workloads and operational pressures in NHS settings
- **Provide citations and sources** for all factual claims, including:
    - UKHSA roles and structure
    - Public health workforce frameworks
    - Public health challenges and reports
    - UK regulatory frameworks (ICO, DHSC, UK GDPR)
    - Public experience and accessibility data
- **For patient and carer personas:**
  - Consider diverse demographics (age, disability, ethnicity, socioeconomic background)
  - Include varying levels of health literacy and digital literacy
  - Reflect real patient/carer experiences and challenges in navigating NHS services
  - Consider accessibility requirements and barriers to access
  - Include mental capacity and cognitive considerations where relevant
  - Cite patient experience data and research where available

### Scenario Details

PASTE SCENARIO HERE