---
marp: true
theme: default
paginate: true
size: 16:9
style: |
  section, section * {
    box-sizing: border-box;
  }

  section {
    display: flex;
    flex-direction: column;
    padding: 0.7rem;
    font-size: 0.8rem;
    height: 100%;
    overflow: hidden;
    position: relative;
  }

  section > h1 {
    margin-left: 1.5cm !important;
    margin-top: 50px !important;
    position: relative;
    top: 20px;
  }
  
  .persona-header {
    text-align: center;
    margin-bottom: 0.8rem;
    flex-shrink: 0;
  }
  
  .persona-title {
    font-size: 2rem;
    font-weight: bold;
    color: #007C91;
    margin: 0;
    text-transform: uppercase;
    letter-spacing: 1px;
  }
  
  .persona-subtitle {
    font-size: 0.8rem;
    color: #666;
    margin: 0.2rem 0 0 0;
    text-transform: uppercase;
    letter-spacing: 1px;
  }
  
  .persona-content {
    display: flex;
    gap: 1rem;
    flex: 1;
    margin-top: 0.6cm;
    min-height: 0;
    align-items: flex-start;
    height: 100%;
  }

  .profile-column {
    flex: 0 0 260px;
    display: flex;
    flex-direction: column;
    gap: 0.6rem;
    margin-left: 1cm;
    margin-top: 0;
    height: 100%;
    min-height: 0;
    max-height: 100%;
    align-self: stretch;
    overflow: hidden;
  }
  
  .profile-section {
    background: white;
    border: 3px solid #007C91;
    border-radius: 10px;
    padding: 0.6rem 0.75rem;
    text-align: center;
    box-shadow: 0 2px 6px rgba(0,0,0,0.1);
    display: flex;
    flex-direction: column;
    gap: 0.5rem;
    flex: 1 1 0;
    min-height: 0;
    overflow: hidden;
  }
  
  .sample-text {
    font-size: 0.55rem;
    color: #999;
    margin-bottom: 0.4rem;
    background: #007C91;
    padding: 0.1rem 0.3rem;
    border-radius: 3px;
  }
  
  .profile-section img {
    width: 120px;
    height: 120px;
    border-radius: 50%;
    border: 2px solid #007C91;
    margin: 0 auto;
    flex-shrink: 0;
    object-fit: cover;
  }
  
  .persona-name {
    font-size: 1.1rem;
    font-weight: bold;
    color: #007C91;
    margin: 0.2rem 0 0.1rem 0;
    text-transform: uppercase;
    line-height: 1.0;
    flex-shrink: 0;
  }
  
  .job-title {
    color: #666;
    font-weight: bold;
    margin-bottom: 0.4rem;
    font-size: 0.75rem;
    line-height: 1.0;
    flex-shrink: 0;
  }
  
  .personal-data {
    flex: 1 1 0;
    overflow-y: auto;
    padding-right: 0.2rem;
    display: flex;
    flex-direction: column;
    gap: 0.4rem;
    min-height: 4rem;
  }

  .personal-data > div {
    background: transparent;
    border-radius: 0;
    padding: 0;
    border-left: none;
    box-shadow: none;
  }

  .personal-data h4 {
    color: #007C91;
    font-size: 0.75rem;
    margin: 0;
    text-transform: uppercase;
    text-align: left;
    letter-spacing: 0.5px;
  }

  .persona-facts {
    display: flex;
    flex-direction: column;
    gap: 0.25rem;
  }

  .data-item {
    display: flex;
    align-items: flex-start;
    gap: 0.35rem;
    text-align: left;
    font-size: 0.68rem;
    line-height: 1.2;
    color: #1f1f1f;
  }

  .data-icon {
    font-size: 0.8rem;
    line-height: 1;
    flex-shrink: 0;
    margin-top: 0.1rem;
  }

  .data-item span:last-child {
    flex: 1;
  }

  .personal-data ul {
    text-align: left;
    font-size: 0.7rem;
    line-height: 1.2;
    margin: 0;
    padding-left: 0.9rem;
  }

  .personal-data li {
    margin-bottom: 0.2rem;
    color: #333;
  }
  
  .content-columns {
    display: flex;
    flex: 1;
    gap: 1.2rem;
    min-height: 0;
  }
  
  .content-single-column {
    flex: 1;
    display: flex;
    flex-direction: column;
    gap: 0.6rem;
    margin-top: 0;
    min-height: 0;
  }
  
  .content-column {
    flex: 1;
    display: flex;
    flex-direction: column;
    min-height: 0;
  }
  
  .content-box {
    background: rgba(0, 94, 184, 0.05);
    border-radius: 6px;
    padding: 0.7rem;
    margin-bottom: 0.6rem;
    border-left: 3px solid #007C91;
    flex-shrink: 0;
  }

  .boxed-section {
    border: 3px solid #007C91;
  }

  .section-title {
    font-size: 0.9rem;
    font-weight: bold;
    color: #007C91;
    margin: 0 0 0.4rem 0;
    text-transform: uppercase;
    line-height: 1.1;
  }
  
  .content-box ul {
    margin: 0;
    padding-left: 0.8rem;
  }
  
  .content-box li {
    margin-bottom: 0.2rem;
    line-height: 1.1;
    color: #333;
    font-size: 0.75rem;
  }
  
  .skills-box {
    background: rgba(0, 94, 184, 0.05);
    border-radius: 8px;
    padding: 1rem;
    border-left: 3px solid #007C91;
    flex: 1;
    min-height: 0;
  }
  
  .skill-item {
    display: flex;
    justify-content: space-between;
    align-items: center;
    margin: 0.4rem 0;
    font-size: 0.8rem;
  }
  
  .stars {
    color: #007C91;
    font-size: 0.9rem;
  }
  
  .systems-grid {
    display: flex;
    justify-content: space-between;
    margin-top: 0.6rem;
    gap: 0.3rem;
  }
  
  .system-badge {
    background: #007C91;
    color: white;
    padding: 0.3rem 0.5rem;
    border-radius: 4px;
    font-weight: bold;
    font-size: 0.7rem;
    text-align: center;
    flex: 1;
  }
  
  .ukhsa-logo-top-right {
    position: absolute;
    top: 0.5cm;
    right: 0.7cm;
    z-index: 2;
  }

  .ukhsa-logo-top-right img {
    height: 45px;
    width: auto;
    object-fit: contain;
  }

---
# APPLICANT — NHS

<div class="ukhsa-logo-top-right">
  <img src="../images/UKHSA-master-logo.jpg" alt="UKHSA logo" />
</div>

<div class="persona-content">

<div class="profile-column">
  <div class="profile-section">
  <img src="../images/amir-covid-programme-coordinator.jpg" alt="Profile Photo" />
    <h2 class="persona-name">Amir Siddiqui</h2>
    <p class="job-title">COVID-19 Programme Coordinator - Primary Care Network</p>
    <div class="personal-data">
      <div>
        <h4>Professional Data</h4>
        <div class="persona-facts">
          <div class="data-item"><span class="data-icon">👤</span><span>4 years in COVID-19 vaccination programmes, recently transitioned to routine work</span></div>
          <div class="data-item"><span class="data-icon">📍</span><span>North West England</span></div>
          <div class="data-item"><span class="data-icon">💼</span><span>Primary Care Network / COVID-19 Vaccination Hub</span></div>
        </div>
      </div>
      <div>
        <h4>Role & Background</h4>
        <ul>
          <li>Started in emergency COVID response; now coordinator for local PCN</li>
          <li>Manages vaccine ordering for 8 GP practices in PCN</li>
          <li>Transitioned from temporary to permanent NHS role</li>
          <li>Works with vaccination teams across multiple locations</li>
          <li>Gained practical ImmForm knowledge during COVID surge</li>
        </ul>
      </div>
    </div>
  </div>

</div>

<div class="content-single-column">
  
  <div class="content-box boxed-section goals-box">
    <h3 class="section-title">Goals & Desired Outcomes</h3>
    <ul>
          <li>Secure permanent, individual access to ImmForm for PCN vaccine coordination</li>
          <li>Establish standardised ordering process across 8 practices</li>
          <li>Maintain overview of vaccine stock across network</li>
          <li>Support transition from emergency to routine immunisation services</li>
    </ul>
  </div>
  
  <div class="content-box boxed-section">
    <h3 class="section-title">Wants & Needs</h3>
    <ul>
          <li>Permanent account status reflecting permanent NHS role</li>
          <li>Quick registration reflecting existing ImmForm knowledge</li>
          <li>Access that isn't tied to single approver (resilience)</li>
          <li>Monthly reporting on vaccine orders and utilisation</li>
          <li>Clear communication about policy changes affecting vaccine ordering</li>
    </ul>
  </div>

  <div class="content-box boxed-section">
    <h3 class="section-title">Pain Points & Frustrations</h3>
    <ul>
          <li>Started on temporary emergency access; now needs permanent setup</li>
          <li>Current account structure doesn't reflect PCN coordinator role</li>
          <li>Manual registration seems unnecessary given existing usage</li>
          <li>Worries about approval delays affecting vaccine availability</li>
          <li>Lacks clear record of ordering history for audit purposes</li>
    </ul>
  </div>

</div>

</div>



---

# APPLICANT — NHS

<div class="ukhsa-logo-top-right">
  <img src="../images/UKHSA-master-logo.jpg" alt="UKHSA logo" />
</div>

<div class="persona-content">

<div class="profile-column">
  <div class="profile-section">
  <img src="../images/colin-occupational-health-coordinator.jpg" alt="Profile Photo" />
    <h2 class="persona-name">Colin Rafferty</h2>
    <p class="job-title">Occupational Health Coordinator - Occupational Health Service</p>
    <div class="personal-data">
      <div>
        <h4>Professional Data</h4>
        <div class="persona-facts">
          <div class="data-item"><span class="data-icon">👤</span><span>12 years in occupational health including vaccine and travel health programmes</span></div>
          <div class="data-item"><span class="data-icon">📍</span><span>Midlands</span></div>
          <div class="data-item"><span class="data-icon">💼</span><span>Occupational Health Services</span></div>
        </div>
      </div>
      <div>
        <h4>Role & Background</h4>
        <ul>
          <li>Manages occupational health vaccine programmes for large NHS trust</li>
          <li>Provides travel health and routine immunisation services</li>
          <li>Works with 5 occupational health nurses</li>
          <li>Handles 300+ vaccine consultations per year</li>
          <li>Experience with multiple digital health systems</li>
        </ul>
      </div>
    </div>
  </div>

</div>

<div class="content-single-column">
  
  <div class="content-box boxed-section goals-box">
    <h3 class="section-title">Goals & Desired Outcomes</h3>
    <ul>
          <li>Streamline vaccine ordering for occupational health programme</li>
          <li>Ensure compliance with occupational health vaccine guidance</li>
          <li>Maintain detailed records of who received which vaccines and when</li>
          <li>Support team growth without creating new access bottlenecks</li>
    </ul>
  </div>
  
  <div class="content-box boxed-section">
    <h3 class="section-title">Wants & Needs</h3>
    <ul>
          <li>Transparent, documented registration process</li>
          <li>Registration portal that explains why information is needed</li>
          <li>Approval from identifiable individual, not generic helpdesk</li>
          <li>Ability to add team members after initial registration</li>
          <li>Integration with trust email systems for verification</li>
    </ul>
  </div>

  <div class="content-box boxed-section">
    <h3 class="section-title">Pain Points & Frustrations</h3>
    <ul>
          <li>Manual process is slow for a time-sensitive health service</li>
          <li>Each team member change requires full re-registration cycle</li>
          <li>Current system doesn't support delegation or backup access</li>
          <li>No clear escalation path if approvals get stuck</li>
          <li>Paper trail is difficult to audit for compliance purposes</li>
    </ul>
  </div>

</div>

</div>



---

# APPLICANT — NHS

<div class="ukhsa-logo-top-right">
  <img src="../images/UKHSA-master-logo.jpg" alt="UKHSA logo" />
</div>

<div class="persona-content">

<div class="profile-column">
  <div class="profile-section">
  <img src="../images/donna-mpox-specialist-nurse.jpg" alt="Profile Photo" />
    <h2 class="persona-name">Donna Eze</h2>
    <p class="job-title">Mpox Specialist Nurse - Infectious Disease Service</p>
    <div class="personal-data">
      <div>
        <h4>Professional Data</h4>
        <div class="persona-facts">
          <div class="data-item"><span class="data-icon">👤</span><span>15 years in infectious disease nursing; 3 years managing Mpox vaccine programme</span></div>
          <div class="data-item"><span class="data-icon">📍</span><span>London</span></div>
          <div class="data-item"><span class="data-icon">💼</span><span>Infectious Disease Service / Mpox Vaccination Programme</span></div>
        </div>
      </div>
      <div>
        <h4>Role & Background</h4>
        <ul>
          <li>Led development of local Mpox vaccination programme</li>
          <li>Manages vaccine ordering and clinical delivery</li>
          <li>Works with sexual health clinics, GUM services, and community partners</li>
          <li>Trains other clinicians on Mpox vaccine protocols</li>
          <li>Established good relationships with ImmForm support team</li>
        </ul>
      </div>
    </div>
  </div>

</div>

<div class="content-single-column">
  
  <div class="content-box boxed-section goals-box">
    <h3 class="section-title">Goals & Desired Outcomes</h3>
    <ul>
          <li>Maintain reliable vaccine supply for Mpox prevention programme</li>
          <li>Support expansion of vaccination to community partners</li>
          <li>Ensure accurate records of vaccine stock and utilisation</li>
          <li>Share best practice on Mpox ordering across region</li>
    </ul>
  </div>
  
  <div class="content-box boxed-section">
    <h3 class="section-title">Wants & Needs</h3>
    <ul>
          <li>Fast-track registration reflecting programme lead status and experience</li>
          <li>Support for adding partner organisations as authorised users</li>
          <li>Regular stock notifications and reorder reminders</li>
          <li>Benchmarking data on regional Mpox vaccine uptake</li>
          <li>Direct escalation contact for urgent supply issues</li>
    </ul>
  </div>

  <div class="content-box boxed-section">
    <h3 class="section-title">Pain Points & Frustrations</h3>
    <ul>
          <li>Current registration process doesn't reflect programme coordinator role</li>
          <li>Cannot easily grant access to partner organisations</li>
          <li>Manual process delays vaccine delivery when stock runs low</li>
          <li>No visibility into other services' Mpox vaccine orders</li>
          <li>Approval delays have previously caused stock shortages</li>
    </ul>
  </div>

</div>

</div>



---

# APPLICANT — NHS

<div class="ukhsa-logo-top-right">
  <img src="../images/UKHSA-master-logo.jpg" alt="UKHSA logo" />
</div>

<div class="persona-content">

<div class="profile-column">
  <div class="profile-section">
  <img src="../images/keisha-sexual-health-administrator.jpg" alt="Profile Photo" />
    <h2 class="persona-name">Keisha Mensah</h2>
    <p class="job-title">Sexual Health Administrator - Sexual Health Service</p>
    <div class="personal-data">
      <div>
        <h4>Professional Data</h4>
        <div class="persona-facts">
          <div class="data-item"><span class="data-icon">👤</span><span>5 years in sexual health services administration and immunisation coordination</span></div>
          <div class="data-item"><span class="data-icon">📍</span><span>Greater London</span></div>
          <div class="data-item"><span class="data-icon">💼</span><span>Sexual Health Service / Immunisation Team</span></div>
        </div>
      </div>
      <div>
        <h4>Role & Background</h4>
        <ul>
          <li>Works in dedicated sexual health service in urban area</li>
          <li>Manages HPV and other immunisation programmes for 3+ clinic locations</li>
          <li>Coordinates with 8 clinical staff and 2 consultants</li>
          <li>Recently joined team; still getting up to speed on ImmForm</li>
          <li>Currently shares access via clinic manager's account</li>
        </ul>
      </div>
    </div>
  </div>

</div>

<div class="content-single-column">
  
  <div class="content-box boxed-section goals-box">
    <h3 class="section-title">Goals & Desired Outcomes</h3>
    <ul>
          <li>Gain independent ImmForm access for clinic vaccine ordering</li>
          <li>Maintain clear audit trail for HPV and other sexual health vaccines</li>
          <li>Support vaccine programme expansion across multiple clinic sites</li>
          <li>Reduce dependency on clinic manager for routine ordering</li>
    </ul>
  </div>
  
  <div class="content-box boxed-section">
    <h3 class="section-title">Wants & Needs</h3>
    <ul>
          <li>Clear guidance on account numbers and organisation codes for each clinic</li>
          <li>Ability to request access quickly when starting new role</li>
          <li>Notification when orders are fulfilled</li>
          <li>Option to delegate to backup staff during leave periods</li>
          <li>Mobile-friendly access to check vaccine stock</li>
    </ul>
  </div>

  <div class="content-box boxed-section">
    <h3 class="section-title">Pain Points & Frustrations</h3>
    <ul>
          <li>Doesn't yet know the correct ImmForm account numbers for clinic locations</li>
          <li>Current PDF form process feels outdated for a modern NHS service</li>
          <li>Cannot access system independently; creates bottleneck for clinic</li>
          <li>Approval delays mean vaccine shortages when clinic runs low on stock</li>
          <li>No training provided on ImmForm as part of onboarding</li>
    </ul>
  </div>

</div>

</div>



---

# APPLICANT — NHS

<div class="ukhsa-logo-top-right">
  <img src="../images/UKHSA-master-logo.jpg" alt="UKHSA logo" />
</div>

<div class="persona-content">

<div class="profile-column">
  <div class="profile-section">
  <img src="../images/priya-vaccination-coordinator.jpg" alt="Profile Photo" />
    <h2 class="persona-name">Priya Chandrasekaran</h2>
    <p class="job-title">Vaccination Coordinator - GP Practice</p>
    <div class="personal-data">
      <div>
        <h4>Professional Data</h4>
        <div class="persona-facts">
          <div class="data-item"><span class="data-icon">👤</span><span>8 years managing immunisation programmes across primary care</span></div>
          <div class="data-item"><span class="data-icon">📍</span><span>South West England</span></div>
          <div class="data-item"><span class="data-icon">💼</span><span>GP Practice / Immunisation Services</span></div>
        </div>
      </div>
      <div>
        <h4>Role & Background</h4>
        <ul>
          <li>Manages vaccine ordering for 12,000-patient GP practice</li>
          <li>Been in practice for 8 years, started as receptionist</li>
          <li>Coordinates with 4 practice nurses and 2 GPs on immunisation schedules</li>
          <li>Currently handles ImmForm access via delegated GP account</li>
          <li>Processes 200+ vaccine orders per year across seasonal flu, COVID-19, and routine programmes</li>
        </ul>
      </div>
    </div>
  </div>

</div>

<div class="content-single-column">
  
  <div class="content-box boxed-section goals-box">
    <h3 class="section-title">Goals & Desired Outcomes</h3>
    <ul>
          <li>Get faster access to ImmForm for her team without waiting for GP approval</li>
          <li>Reduce administrative burden of managing account changes</li>
          <li>Have visibility into vaccine stock levels and ordering history</li>
          <li>Ensure compliance with UKHSA vaccine ordering guidelines</li>
    </ul>
  </div>
  
  <div class="content-box boxed-section">
    <h3 class="section-title">Wants & Needs</h3>
    <ul>
          <li>Simple, clear registration process that doesn't require IT support</li>
          <li>Email confirmation that her application has been received</li>
          <li>Quick approval turnaround (2-3 days preferred)</li>
          <li>Individual user account rather than shared practice account</li>
          <li>Audit trail of who ordered what and when</li>
    </ul>
  </div>

  <div class="content-box boxed-section">
    <h3 class="section-title">Pain Points & Frustrations</h3>
    <ul>
          <li>Currently waits 5-7 days for account approval via manual PDF process</li>
          <li>Cannot order vaccines when the authorised person is on leave</li>
          <li>Manual form-filling is error-prone; re-keying data creates mistakes</li>
          <li>No visibility into application status while it's being processed</li>
          <li>Shared account access means no accountability for individual vaccine orders</li>
    </ul>
  </div>

</div>

</div>



---

# APPLICANT — NON-NHS

<div class="ukhsa-logo-top-right">
  <img src="../images/UKHSA-master-logo.jpg" alt="UKHSA logo" />
</div>

<div class="persona-content">

<div class="profile-column">
  <div class="profile-section">
  <img src="../images/marcus-procurement-compliance-lead.jpg" alt="Profile Photo" />
    <h2 class="persona-name">Marcus Obi</h2>
    <p class="job-title">Procurement and Compliance Lead - Authorised Wholesaler</p>
    <div class="personal-data">
      <div>
        <h4>Professional Data</h4>
        <div class="persona-facts">
          <div class="data-item"><span class="data-icon">👤</span><span>10 years in pharmaceutical procurement and supply chain compliance</span></div>
          <div class="data-item"><span class="data-icon">📍</span><span>South East England</span></div>
          <div class="data-item"><span class="data-icon">💼</span><span>Authorised Wholesaler / Procurement Operations</span></div>
        </div>
      </div>
      <div>
        <h4>Role & Background</h4>
        <ul>
          <li>Works for licensed authorised pharmaceutical wholesaler</li>
          <li>Manages vaccine procurement and distribution to NHS sites</li>
          <li>Responsible for regulatory compliance and audit trails</li>
          <li>Coordinates with MHRA and wholesaler management</li>
          <li>Manages 20+ vaccine product lines and SKUs</li>
        </ul>
      </div>
    </div>
  </div>

</div>

<div class="content-single-column">
  
  <div class="content-box boxed-section goals-box">
    <h3 class="section-title">Goals & Desired Outcomes</h3>
    <ul>
          <li>Maintain compliant, auditable ordering process for vaccine procurement</li>
          <li>Support wholesaler's NHS supply chain relationships</li>
          <li>Ensure traceability from order to delivery to end-user</li>
          <li>Scale operations without increasing administrative burden</li>
    </ul>
  </div>
  
  <div class="content-box boxed-section">
    <h3 class="section-title">Wants & Needs</h3>
    <ul>
          <li>Compliant digital registration that meets MHRA expectations</li>
          <li>Clear audit trail of all ordering transactions</li>
          <li>Approval from NHS stakeholders who understand wholesaler role</li>
          <li>Integration with wholesaler's compliance reporting systems</li>
          <li>Notifications of supply or regulatory changes affecting orders</li>
    </ul>
  </div>

  <div class="content-box boxed-section">
    <h3 class="section-title">Pain Points & Frustrations</h3>
    <ul>
          <li>Current PDF process creates compliance gaps in audit trail</li>
          <li>Manual re-keying increases risk of ordering errors</li>
          <li>Non-NHS status sometimes causes confusion in registration process</li>
          <li>Approval timescales are unpredictable for supply chain planning</li>
          <li>No integration with wholesaler's internal systems creates duplicate data entry</li>
    </ul>
  </div>

</div>

</div>



---

# APPLICANT — NON-NHS

<div class="ukhsa-logo-top-right">
  <img src="../images/UKHSA-master-logo.jpg" alt="UKHSA logo" />
</div>

<div class="persona-content">

<div class="profile-column">
  <div class="profile-section">
  <img src="../images/sanjay-immunoglobulin-pharmacist.jpg" alt="Profile Photo" />
    <h2 class="persona-name">Sanjay Patel</h2>
    <p class="job-title">Pharmacy Lead - Immunoglobulin Holding Centre</p>
    <div class="personal-data">
      <div>
        <h4>Professional Data</h4>
        <div class="persona-facts">
          <div class="data-item"><span class="data-icon">👤</span><span>18 years as pharmacist; 8 years managing immunoglobulin distribution centre</span></div>
          <div class="data-item"><span class="data-icon">📍</span><span>West Midlands</span></div>
          <div class="data-item"><span class="data-icon">💼</span><span>Immunoglobulin Holding Centre / Pharmacy Services</span></div>
        </div>
      </div>
      <div>
        <h4>Role & Background</h4>
        <ul>
          <li>Licensed pharmacist managing dedicated immunoglobulin distribution service</li>
          <li>Serves 40+ NHS sites with specialist immunoglobulin products</li>
          <li>Manages temperature-controlled storage and complex logistics</li>
          <li>Responsible for product tracking and MHRA compliance</li>
          <li>Operates under strict pharmaceutical regulations</li>
        </ul>
      </div>
    </div>
  </div>

</div>

<div class="content-single-column">
  
  <div class="content-box boxed-section goals-box">
    <h3 class="section-title">Goals & Desired Outcomes</h3>
    <ul>
          <li>Maintain secure, compliant immunoglobulin supply chain</li>
          <li>Ensure rapid response to NHS clinical emergencies</li>
          <li>Meet all MHRA and GDP (Good Distribution Practice) requirements</li>
          <li>Scale services without compromising traceability or compliance</li>
    </ul>
  </div>
  
  <div class="content-box boxed-section">
    <h3 class="section-title">Wants & Needs</h3>
    <ul>
          <li>Robust registration process that demonstrates competence and compliance</li>
          <li>Approval from NHS stakeholders familiar with immunoglobulin supply model</li>
          <li>Integration with patient-level traceability requirements</li>
          <li>Notification system for urgent supply requests or recalls</li>
          <li>Compliance-ready audit trail for MHRA inspections</li>
    </ul>
  </div>

  <div class="content-box boxed-section">
    <h3 class="section-title">Pain Points & Frustrations</h3>
    <ul>
          <li>Current system doesn't adequately reflect specialist, regulated nature of role</li>
          <li>Manual processing risks breaking strict compliance chains</li>
          <li>Approval delays can impact clinical delivery of life-saving products</li>
          <li>No integration with pharmaceutical tracking systems</li>
          <li>Cannot delegate to backup staff if primary user is unavailable</li>
    </ul>
  </div>

</div>

</div>



---

# AUTHORISED PERSON

<div class="ukhsa-logo-top-right">
  <img src="../images/UKHSA-master-logo.jpg" alt="UKHSA logo" />
</div>

<div class="persona-content">

<div class="profile-column">
  <div class="profile-section">
  <img src="../images/linda-authorised-person.jpg" alt="Profile Photo" />
    <h2 class="persona-name">Linda Forsythe</h2>
    <p class="job-title">Practice Manager and Authorised Person - GP Practice</p>
    <div class="personal-data">
      <div>
        <h4>Professional Data</h4>
        <div class="persona-facts">
          <div class="data-item"><span class="data-icon">👤</span><span>20 years in GP practice management; 12 years as Authorised Person for ImmForm</span></div>
          <div class="data-item"><span class="data-icon">📍</span><span>South West England</span></div>
          <div class="data-item"><span class="data-icon">💼</span><span>GP Practice Management</span></div>
        </div>
      </div>
      <div>
        <h4>Role & Background</h4>
        <ul>
          <li>Practice manager for 15-partner GP practice</li>
          <li>Designated as Authorised Person for ImmForm account</li>
          <li>Manages 80+ staff including clinicians and administrative team</li>
          <li>Responsible for practice compliance and governance</li>
          <li>Has approved 200+ user registrations over 12 years</li>
        </ul>
      </div>
    </div>
  </div>

</div>

<div class="content-single-column">
  
  <div class="content-box boxed-section goals-box">
    <h3 class="section-title">Goals & Desired Outcomes</h3>
    <ul>
          <li>Manage user access securely while supporting practice operations</li>
          <li>Maintain clear, auditable approval process</li>
          <li>Reduce time spent on access management tasks</li>
          <li>Support team flexibility without compromising security</li>
    </ul>
  </div>
  
  <div class="content-box boxed-section">
    <h3 class="section-title">Wants & Needs</h3>
    <ul>
          <li>Clear, structured approval process that documents decisions</li>
          <li>Time-bounded approval requests with escalation if delayed</li>
          <li>Visibility into who is accessing ImmForm account and when</li>
          <li>Easy way to revoke access if staff leave or change roles</li>
          <li>Integration with practice HR/staff directory systems</li>
    </ul>
  </div>

  <div class="content-box boxed-section">
    <h3 class="section-title">Pain Points & Frustrations</h3>
    <ul>
          <li>Current email-based process creates cluttered approval trail</li>
          <li>No visibility into what access each person actually has</li>
          <li>Cannot easily track which staff have been approved</li>
          <li>Manual process is time-consuming; often deprioritised</li>
          <li>If approval is forgotten, staff are blocked from vaccine ordering</li>
    </ul>
  </div>

</div>

</div>



---

# IMMFORM HELPDESK (CURRENT STATE)

<div class="ukhsa-logo-top-right">
  <img src="../images/UKHSA-master-logo.jpg" alt="UKHSA logo" />
</div>

<div class="persona-content">

<div class="profile-column">
  <div class="profile-section">
  <img src="../images/david-helpdesk-operative.jpg" alt="Profile Photo" />
    <h2 class="persona-name">David Acheampong</h2>
    <p class="job-title">Helpdesk Operative - ImmForm Support (Current State)</p>
    <div class="personal-data">
      <div>
        <h4>Professional Data</h4>
        <div class="persona-facts">
          <div class="data-item"><span class="data-icon">👤</span><span>6 years in ImmForm helpdesk; formerly healthcare admin</span></div>
          <div class="data-item"><span class="data-icon">📍</span><span>UKHSA National Office</span></div>
          <div class="data-item"><span class="data-icon">💼</span><span>ImmForm Support Team / Helpdesk Operations</span></div>
        </div>
      </div>
      <div>
        <h4>Role & Background</h4>
        <ul>
          <li>ImmForm support team member handling user registrations</li>
          <li>Processes 5-8 registration requests per day during peak periods</li>
          <li>Manually validates data, re-keys forms into ImmForm</li>
          <li>Chases approvals from Authorised Persons via email</li>
          <li>Works with users on account and organisation code validation</li>
        </ul>
      </div>
    </div>
  </div>

</div>

<div class="content-single-column">
  
  <div class="content-box boxed-section goals-box">
    <h3 class="section-title">Goals & Desired Outcomes</h3>
    <ul>
          <li>Process registration applications quickly and accurately</li>
          <li>Reduce time spent on manual data entry and chasing</li>
          <li>Maintain compliance and audit trail for registrations</li>
          <li>Provide good support experience to healthcare users</li>
    </ul>
  </div>
  
  <div class="content-box boxed-section">
    <h3 class="section-title">Wants & Needs</h3>
    <ul>
          <li>Structured input from users with validated data</li>
          <li>Automated validation for account/organisation pairs</li>
          <li>Workflow system to track approval status</li>
          <li>Ability to see approval decision history</li>
          <li>Clear escalation path for complex cases</li>
    </ul>
  </div>

  <div class="content-box boxed-section">
    <h3 class="section-title">Pain Points & Frustrations</h3>
    <ul>
          <li>Manual re-keying is repetitive, error-prone, and time-consuming</li>
          <li>Cannot easily follow up on delayed approvals</li>
          <li>Invalid account/organisation combinations cause rework</li>
          <li>No visibility into application status for users asking about progress</li>
          <li>Email-based chasing is inefficient; approvers don't always respond</li>
    </ul>
  </div>

</div>

</div>



---

# IMMFORM HELPDESK (FALLBACK)

<div class="ukhsa-logo-top-right">
  <img src="../images/UKHSA-master-logo.jpg" alt="UKHSA logo" />
</div>

<div class="persona-content">

<div class="profile-column">
  <div class="profile-section">
  <img src="../images/fatima-helpdesk-case-handler.jpg" alt="Profile Photo" />
    <h2 class="persona-name">Fatima Osei</h2>
    <p class="job-title">Case Handler - ImmForm Support (Digitally-Assisted Fallback)</p>
    <div class="personal-data">
      <div>
        <h4>Professional Data</h4>
        <div class="persona-facts">
          <div class="data-item"><span class="data-icon">👤</span><span>3 years in ImmForm support; transitioned from manual helpdesk to assisted role</span></div>
          <div class="data-item"><span class="data-icon">📍</span><span>UKHSA National Office</span></div>
          <div class="data-item"><span class="data-icon">💼</span><span>ImmForm Support Team / Case Management</span></div>
        </div>
      </div>
      <div>
        <h4>Role & Background</h4>
        <ul>
          <li>ImmForm support case handler; newer member of team</li>
          <li>Works on complex registrations and fallback cases</li>
          <li>Handles edge cases: shared mailboxes, invalid codes, approval delays</li>
          <li>Recently upskilled on digital-assisted case management</li>
          <li>Supports primary helpdesk operators on difficult applications</li>
        </ul>
      </div>
    </div>
  </div>

</div>

<div class="content-single-column">
  
  <div class="content-box boxed-section goals-box">
    <h3 class="section-title">Goals & Desired Outcomes</h3>
    <ul>
          <li>Resolve complex registration cases efficiently and fairly</li>
          <li>Support users who face barriers with standard registration</li>
          <li>Maintain compliance while being flexible for edge cases</li>
          <li>Build expertise in handling difficult scenarios</li>
    </ul>
  </div>
  
  <div class="content-box boxed-section">
    <h3 class="section-title">Wants & Needs</h3>
    <ul>
          <li>Clear decision-making framework for edge cases</li>
          <li>Tools to verify identity and account details</li>
          <li>Access to Authorised Person contact information for escalation</li>
          <li>Digital workflow to track case status and decisions</li>
          <li>Training on regulatory requirements and compliance</li>
    </ul>
  </div>

  <div class="content-box boxed-section">
    <h3 class="section-title">Pain Points & Frustrations</h3>
    <ul>
          <li>No standardised process for handling edge cases</li>
          <li>Limited tools to verify user identity and account details</li>
          <li>Depends on escalation to seniors for difficult decisions</li>
          <li>Manual tracking makes it hard to report on case outcomes</li>
          <li>Difficult users or unclear requirements can get stuck indefinitely</li>
    </ul>
  </div>

</div>

</div>



---

# QA / WDA RESPONSIBLE PERSON

<div class="ukhsa-logo-top-right">
  <img src="../images/UKHSA-master-logo.jpg" alt="UKHSA logo" />
</div>

<div class="persona-content">

<div class="profile-column">
  <div class="profile-section">
  <img src="../images/rachel-qa-wda-responsible-person.jpg" alt="Profile Photo" />
    <h2 class="persona-name">Rachel Thornton</h2>
    <p class="job-title">Quality Assurance Lead and WDA Responsible Person - UKHSA</p>
    <div class="personal-data">
      <div>
        <h4>Professional Data</h4>
        <div class="persona-facts">
          <div class="data-item"><span class="data-icon">👤</span><span>14 years in UKHSA quality assurance and regulatory compliance; 8 years as WDA Responsible Person</span></div>
          <div class="data-item"><span class="data-icon">📍</span><span>UKHSA National Office</span></div>
          <div class="data-item"><span class="data-icon">💼</span><span>Quality Assurance / Regulatory Compliance</span></div>
        </div>
      </div>
      <div>
        <h4>Role & Background</h4>
        <ul>
          <li>UKHSA Quality Assurance and WDA (Wholesale Dealer in Medicines) Responsible Person</li>
          <li>Responsible for regulatory compliance of ImmForm system</li>
          <li>Conducts audits of registration processes and user access</li>
          <li>Manages MHRA inspections and compliance reporting</li>
          <li>Ensures ImmForm meets GDP and traceability requirements</li>
        </ul>
      </div>
    </div>
  </div>

</div>

<div class="content-single-column">
  
  <div class="content-box boxed-section goals-box">
    <h3 class="section-title">Goals & Desired Outcomes</h3>
    <ul>
          <li>Ensure ImmForm registration process meets MHRA and GDP requirements</li>
          <li>Maintain complete, auditable trail of all user lifecycle decisions</li>
          <li>Reduce compliance risk from manual processes</li>
          <li>Support UKHSA governance and regulatory reporting</li>
    </ul>
  </div>
  
  <div class="content-box boxed-section">
    <h3 class="section-title">Wants & Needs</h3>
    <ul>
          <li>System-generated audit trail of all registration decisions</li>
          <li>Clear evidence of identity verification for each user</li>
          <li>Documentation of Authorised Person approval decisions</li>
          <li>Regular compliance reporting and audit trails</li>
          <li>System alerts for any policy or compliance anomalies</li>
    </ul>
  </div>

  <div class="content-box boxed-section">
    <h3 class="section-title">Pain Points & Frustrations</h3>
    <ul>
          <li>Manual email-based process creates gaps in audit trail</li>
          <li>No clear evidence of identity verification steps taken</li>
          <li>Difficult to demonstrate compliance during MHRA inspections</li>
          <li>Cannot easily report on user registration metrics</li>
          <li>Compliance risk from lack of structured data capture</li>
    </ul>
  </div>

</div>

</div>
